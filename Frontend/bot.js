function createGuid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    });
}

const urlParams = new URLSearchParams(window.location.search);
sessionUrl.value = urlParams.get('sessionUrl');
playerName.value = urlParams.get('playerName');
clientId.value = createGuid();

createSession.addEventListener("click", () => {
    const guid = createGuid();
    fetch(sessionUrl.value + "?create=" + guid)
        .then(res => sessionGuid.value = guid);
});
listSessions.addEventListener("click", () =>
    fetch(sessionUrl.value + "?sessions")
        .then(res => res.json())
        .then(res => statustext.value = res.Sessions.map(s => s.Id + " - " + ((s.Players && s.Players.length) || 0).toString()).join('\n')));
joinSession.addEventListener("click", () =>
    fetch(sessionUrl.value + "?join&session=" + sessionGuid.value + "&playerid=" + clientId.value + "&name=" + playerName.value)
        .then(statustext.value = "joined session"));
startSession.addEventListener("click", () =>
    fetch(sessionUrl.value + "?start&session=" + sessionGuid.value)
        .then(statustext.value = "started session"));
showSessionDetails.addEventListener("click", () =>
    fetch(sessionUrl.value + "?session=" + sessionGuid.value)
        .then(res => res.json())
        .then(res => statustext.value = JSON.stringify(res)));
uploadBot.addEventListener("click", () => console.log("Not implemented"));
endAttack.addEventListener("click", () =>
    fetch(sessionUrl.value + "?endmove&session=" + sessionGuid.value + "&playerid=" + clientId.value)
        .then(res => statustext.value = "Attack phase ended"));
endReinforce.addEventListener("click", () =>
    fetch(sessionUrl.value + "?endreinforce&session=" + sessionGuid.value + "&playerid=" + clientId.value)
        .then(res => statustext.value = "Reinforce phase ended"));