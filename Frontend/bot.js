currentState = undefined;
previousTile = undefined;
boardSize = undefined;

function createGuid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

function loadUrlParameters() {
    const urlParams = new URLSearchParams(window.location.search);
    sessionUrl.value = urlParams.get('sessionUrl');
    playerName.value = urlParams.get('playerName');
    clientId.value = createGuid();
}

function createSessionHandler() {
    const guid = createGuid();
    fetch(sessionUrl.value + "?create=" + guid)
        .then(res => sessionGuid.value = guid);
}

function listSessionHandler() {
    fetch(sessionUrl.value + "?sessions")
        .then(res => res.json())
        .then(res => statustext.value = res.Sessions.map(s => s.Id + " - " + ((s.Players && s.Players.length) || 0).toString()).join('\n'));
}

function joinSessionHandler() {
    fetch(sessionUrl.value + "?join&session=" + sessionGuid.value + "&playerid=" + clientId.value + "&name=" + playerName.value)
        .then(statustext.value = "joined session");
}

function startSessionHandler() {
    fetch(sessionUrl.value + "?start&session=" + sessionGuid.value)
        .then(statustext.value = "started session");
}

function drawRect(x, y, canvas, text, color) {
    var ctx = canvas.getContext("2d");
    var width = canvas.width / 6;
    var height = canvas.height / 6;
    ctx.beginPath();
    ctx.strokeStyle = "#000000";
    ctx.fillStyle = color;
    ctx.lineWidth = 4;
    ctx.rect(x * width, y * height, width, height);
    ctx.stroke();
    ctx.fill();
    ctx.font = "20px Georgia";
    ctx.textAlign = "center";
    ctx.textBaseline = "middle";
    ctx.fillStyle = "#000000";
    ctx.fillText(text, (x + 0.5) * width, (y + 0.5) * height);
}

function drawBoard(board, canvasId) {
    var canvas = document.getElementById(canvasId);
    for (var y = 0; y < 6; ++y) {
        for (var x = 0; x < 6; ++x) {
            var tile = board.TileRows[y].Tiles[x];
            var color = tile.OwnerColorRgbCsv ? tile.OwnerColorRgbCsv : "150,150,150";
            var text = tile.NumTroops == 0 ? "" : "" + tile.NumTroops;
            drawRect(x, y, canvas, text, "rgb(" + color + ")");
        }
    }
}

function addStat(stats, text, marginLeft, color, fontWeight) {
    var span = document.createElement("span");
    span.innerHTML = text;
    marginLeft && (span.style.marginLeft = marginLeft);
    color && (span.style.color = "rgb(" + color + ")");
    fontWeight && (span.style.fontWeight = fontWeight);
    stats.appendChild(span);
    stats.appendChild(document.createElement("br"));
}

function writeStats(session, statsId) {
    var stats = document.getElementById(statsId);
    stats.innerHTML = "";
    session.Players.forEach(player => {
        var text = player.Name;
        var color = player.ColorRgbCsv;
        if (session.GameState.CurrentPlayer.Id === player.Id) {
            text += " <- This players turn";
            var fontWeight = "bold";
        }
        addStat(stats, text, undefined, color, fontWeight);
        var tiles = session.CurrentBoard.TilesOfPlayers[player.Id];
        addStat(stats, "Tiles: " + tiles.length, 20);
        addStat(stats, "Troops: " + tiles.map(t => t.NumTroops).reduce((total, current) => total + current, 0), 20);
    });
}

function setState(gameState) {
    if (gameState.CurrentPlayer.Id == clientId.value)
        currentState = gameState.PlayerPhase;
    else
        currentState = undefined;
}

function showSessionDetailsHandler() {
    fetch(sessionUrl.value + "?session=" + sessionGuid.value)
        .then(res => res.json())
        .then(res => {
            statustext.value = JSON.stringify(res);
            tmp = res;
            if (res.Session.CurrentBoard) {
                drawBoard(res.Session.CurrentBoard, "gameboard1");
                writeStats(res.Session, "gamestats1");
                setState(res.Session.GameState);
                boardSize = res.Session.RuleSet.BoardSize;
            }
        });
}

function uploadBotHandler() {
    console.log("Not implemented");
}

function endAttackHandler() {
    fetch(sessionUrl.value + "?endmove&session=" + sessionGuid.value + "&playerid=" + clientId.value)
        .then(res => statustext.value = "Attack phase ended");
}

function endReinforceHandler() {
    fetch(sessionUrl.value + "?endreinforce&session=" + sessionGuid.value + "&playerid=" + clientId.value)
        .then(res => statustext.value = "Reinforce phase ended");
}

function buildTileId(x, y, boardSize) {
    return x + (y * boardSize);
}

function reinforce(x, y) {
    var reinforceUrl = sessionUrl.value + "?reinforce&session=" + sessionGuid.value + "&playerid=" + clientId.value + "&tileid=" + buildTileId(x, y, boardSize);
    fetch(reinforceUrl)
        .then(res => statustext.value = JSON.stringify(res));
    showSessionDetailsHandler();
}

function attack(x, y, fromX, fromY) {
    var attackUrl = sessionUrl.value + "?move&session=" + sessionGuid.value + "&playerid=" + clientId.value + "&from=" + buildTileId(fromX, fromY, 6) + "&to=" + buildTileId(x, y, boardSize);
    fetch(attackUrl)
        .then(res => statustext.value = JSON.stringify(res));
    showSessionDetailsHandler();
}

function canvasClickHandler(e) {
    if (currentState != "Move/attack" && currentState != "Reinforce")
        return;
    var tileWidth = e.target.width / 6;
    var tileHeight = e.target.height / 6;
    var t = e.target.parentElement;
    var x = Math.floor((e.clientX - e.target.offsetLeft) / tileWidth);
    var y = Math.floor((e.clientY - e.target.offsetTop) / tileHeight);
    console.log("x: " + x + ", y: " + y);
    if (currentState == "Reinforce") {
        previousTile = undefined;
        reinforce(x, y);
    } else if (previousTile) {
        attack(x, y, previousTile.x, previousTile.y);
        previousTile = undefined;
    } else {
        previousTile = { x: x, y: y };
    }
}

function bindClickHandlers() {
    createSession.addEventListener("click", createSessionHandler);
    listSessions.addEventListener("click", listSessionHandler);
    joinSession.addEventListener("click", joinSessionHandler);
    startSession.addEventListener("click", startSessionHandler);
    showSessionDetails.addEventListener("click", showSessionDetailsHandler);
    uploadBot.addEventListener("click", uploadBotHandler);
    endAttack.addEventListener("click", endAttackHandler);
    endReinforce.addEventListener("click", endReinforce);
    gameboard1.addEventListener("click", canvasClickHandler);
}

function setupCanvas() {
    gameboard1.width = gameboard1.parentElement.clientWidth;
    gameboard1.height = gameboard1.parentElement.clientHeight;
}

function initialize() {
    loadUrlParameters();
    setupCanvas();
    bindClickHandlers();
    setInterval(function () { }, 3000);
}