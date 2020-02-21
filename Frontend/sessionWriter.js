function doEndAttackPhase(sessionUrl, sessionGuid, clientId) {
    return fetch(sessionUrl + "?endmove&session=" + sessionGuid + "&playerid=" + clientId)
}

function doEndReinforcePhase(sessionUrl, sessionGuid, clientId) {
    return fetch(sessionUrl + "?endreinforce&session=" + sessionGuid + "&playerid=" + clientId)
}

function doJoinSession(sessionUrl, sessionGuid, clientId, playerName) {
    return fetch(sessionUrl + "?join&session=" + sessionGuid + "&playerid=" + clientId + "&name=" + playerName);
}

function doCreateSession(sessionUrl, guid) {
    return fetch(sessionUrl + "?create=" + guid);
}