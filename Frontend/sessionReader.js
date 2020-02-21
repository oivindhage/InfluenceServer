
function isOngoing(response) {
    return response.Session.GameState.GamePhase == "Ongoing";
}

function isOurTurn(response) {
    return response.Session.GameState.CurrentPlayer.Id == clientId.value;
}

function isAttack(response) {
    return response.Session.GameState.PlayerPhase == "Move/attack";
}

function isReinforce(response) {
    return response.Session.GameState.PlayerPhase == "Reinforce";
}

function getMyTiles(response) {
    return response.Session.CurrentBoard.AllTiles.filter(t => t.OwnerId == clientId.value);
}

function getMyReinforceableTiles(response) {
    return getMyTiles(response).filter(t => t.NumTroops < getRuleSet(response).MaxNumTroopsInTile);
}

function getRuleSet(response) {
    return response.Session.RuleSet;
}

function getBoardSize(response) {
    return getRuleSet(response).BoardSize;
}

function getAllTiles(response) {
    return response.Session.CurrentBoard.AllTiles;
}

function getAttackTargets(response, attackingTile) {
    return getNeighborTiles(response, attackingTile).filter(n => n.OwnerId != attackingTile.OwnerId);
}

function getMyTilesThatCanAttack(response) {
    return getMyTiles(response).filter(t => t.NumTroops > 1).filter(t => getAttackTargets(response, t).length > 0);
}

function canReinforce(response) {
    return response.Session.GameState.CurrentPlayer.NumAvailableReinforcements > 0;
}
