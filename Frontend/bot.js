currentState = undefined;
previousTile = undefined;
boardSize = undefined;
counter = 0;
images = undefined;

function loadImages() {
    images = [];
    for (var i = 0; i < 10; ++i) {
        var img = new Image();
        if (i < 5)
            img.src = (i + 1) + 'a.png';
        else
            img.src = ((i % 5) + 1) + 'b.png';
        images.push(img);
    }
}

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
    doCreateSession(sessionUrl.value, guid)
        .then(response => sessionGuid.value = guid);
}

function listSessionHandler() {
    fetch(sessionUrl.value + "?sessions")
        .then(response => response.json())
        .then(response => {
            statustext.value = response.Sessions.map(s => s.Id + " - " + ((s.Players && s.Players.length) || 0).toString()).join('\n');
            if (response.Sessions && response.Sessions.length > 0)
                sessionGuid.value = response.Sessions[response.Sessions.length - 1].Id;
        });
}

function joinSessionHandler() {
    doJoinSession(sessionUrl.value, sessionGuid.value, clientId.value, playerName.value)
        .then(statustext.value = "joined session");
}

function startSessionHandler() {
    fetch(sessionUrl.value + "?start&session=" + sessionGuid.value)
        .then(statustext.value = "started session");
}

function drawRect(x, y, canvas, spritenumber, color) {
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

    if (spritenumber > -1) {
        var imageData = images[spritenumber];

        ctx.drawImage(images[spritenumber], x * width, y * height, width, height);

    }
}

function drawBoard(board, canvasId) {
    counter = (counter + 1) % 2;
    spritestate = counter * 5;
    var canvas = document.getElementById(canvasId);
    for (var y = 0; y < 6; ++y) {
        for (var x = 0; x < 6; ++x) {
            var tile = board.TileRows[y].Tiles[x];
            var color = tile.OwnerColorRgbCsv ? tile.OwnerColorRgbCsv : "150,150,150";
            drawRect(x, y, canvas, tile.NumTroops ? (tile.NumTroops - 1) + spritestate : -1, "rgb(" + color + ")");
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

function getSessionState() {
    return fetch(sessionUrl.value + "?session=" + sessionGuid.value)
        .then(res => res.json());
}

function update(response) {
    statustext.value = JSON.stringify(response);
    if (response.Session.CurrentBoard) {
        drawBoard(response.Session.CurrentBoard, "gameboard1");
        writeStats(response.Session, "gamestats1");
        setState(response.Session.GameState);
        boardSize = getBoardSize(response);
    }

}

function showSessionDetailsHandler() {
    getSessionState().then(response => update(response));
}

function uploadBotHandler() {
    console.log("Not implemented");
}

function endAttackHandler() {
    doEndAttackPhase(sessionUrl.value, sessionGuid.value, clientId.value)
        .then(res => statustext.value = "Attack phase ended");
}

function endReinforceHandler() {
    doEndReinforcePhase(sessionUrl.value, sessionGuid.value, clientId.value)
        .then(res => statustext.value = "Reinforce phase ended");
}

function buildTileId(x, y, boardSize) {
    return x + (y * boardSize);
}

function getNeighborTiles(response, attackingTile) {
    return getAllTiles(response)
        .filter(t => t.X == attackingTile.X || t.Y == attackingTile.Y)
        .filter(t => Math.abs(t.X - attackingTile.X) == 1 || Math.abs(t.Y - attackingTile.Y) == 1);
}

function getRandomElement(array) {
    return array[Math.floor(Math.random() * array.length)]
}

function doBotAttackAction(response) {
    var myTiles = getMyTilesThatCanAttack(response);
    if (!myTiles.length) {
        doEndAttackPhase(sessionUrl.value, sessionGuid.value, clientId.value);
        return;
    }
    var attacker = getRandomElement(myTiles);
    var targets = getAttackTargets(response, attacker);
    var target = getRandomElement(targets);
    attack(target.X, target.Y, attacker.X, attacker.Y, getBoardSize(response));
}

function doBotReinforceAction(response) {
    var reinforceableTiles = getMyReinforceableTiles(response);
    if (reinforceableTiles.length && canReinforce(response)) {
        var reinforceTile = getRandomElement(reinforceableTiles);
        reinforce(reinforceTile.X, reinforceTile.Y, getBoardSize(response));
    }
    else
        doEndReinforcePhase(sessionUrl.value, sessionGuid.value, clientId.value);
}

function doBotAction() {
    getSessionState()
        .then(response => {
            if (isOngoing(response) && isOurTurn(response)) {
                console.log("It's our turn");
                if (isAttack(response)) {
                    doBotAttackAction(response);
                }
                else if (isReinforce(response)) {
                    doBotReinforceAction(response);
                }
            } else {
                update(response);
            }
        });
}

intervalHandle = -1;
function playAsBotHandler() {
    console.log(playAsBot.checked);
    if (playAsBot.checked) {
        count = 0;
        intervalHandle = setInterval(doBotAction, 100);
    } else {
        clearInterval(intervalHandle);
    }
}

function reinforce(x, y, boardSize) {
    var reinforceUrl = sessionUrl.value + "?reinforce&session=" + sessionGuid.value + "&playerid=" + clientId.value + "&tileid=" + buildTileId(x, y, boardSize);
    fetch(reinforceUrl)
        .then(res => statustext.value = JSON.stringify(res));
    showSessionDetailsHandler();
}

function attack(x, y, fromX, fromY, boardSize) {
    var attackUrl = sessionUrl.value + "?move&session=" + sessionGuid.value + "&playerid=" + clientId.value + "&from=" + buildTileId(fromX, fromY, boardSize) + "&to=" + buildTileId(x, y, boardSize);
    fetch(attackUrl)
        .then(res => statustext.value = JSON.stringify(res));
    showSessionDetailsHandler();
}

function canvasClickHandler(e) {
    if (currentState != "Move/attack" && currentState != "Reinforce")
        return;
    var tileWidth = e.target.width / boardSize;
    var tileHeight = e.target.height / boardSize;
    var t = e.target.parentElement;
    var x = Math.floor((e.clientX - e.target.offsetLeft) / tileWidth);
    var y = Math.floor((e.clientY - e.target.offsetTop) / tileHeight);
    console.log("x: " + x + ", y: " + y);
    if (currentState == "Reinforce") {
        previousTile = undefined;
        reinforce(x, y, boardSize);
    } else if (previousTile) {
        attack(x, y, previousTile.x, previousTile.y, boardSize);
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
    endReinforce.addEventListener("click", endReinforceHandler);
    gameboard1.addEventListener("click", canvasClickHandler);
    playAsBot.addEventListener("click", playAsBotHandler);
}

function setupCanvas() {
    gameboard1.width = gameboard1.parentElement.clientWidth;
    gameboard1.height = gameboard1.parentElement.clientHeight;
}

function initialize() {
    loadUrlParameters();
    setupCanvas();
    bindClickHandlers();
    loadImages();
    setInterval(function () { }, 3000);
}

initialize();