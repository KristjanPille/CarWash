let gameState = ["", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""];
let playerOneTake = 0;
let playerTwoTake = 0;
let lastIndexOne = 0;
let lastIndexTwo = 0;
let winner = "";
let computer = false;
let currentindex = 0;
let gameActive = true;
let dropFaze = true;
let currentPlayer = "X";
let playerturn = 0;
let computerVsComputer = false;

const winningMessage = () => `${winner} has won!`;
const currentPlayerTurn = () => `${currentPlayer}'s turn`;
const playerTakes = () => `Player one´s takes: ${playerOneTake} | Player two´s takes: ${playerTwoTake}`;

const statusDisplay = document.querySelector('.game--status');
const takesDisplay = document.querySelector('.takes');

function handleCellClick(clickedCellEvent: any) {
    const clickedCell = clickedCellEvent.target;
    const clickedCellIndex = parseInt(clickedCell.getAttribute('id'));
    if(dropFaze == true && computerVsComputer == false){
        if (gameState[clickedCellIndex] !== "" || !gameActive) {
            return;
        }
        if(gameState[clickedCellIndex+1] == currentPlayer && gameState[clickedCellIndex-1] == currentPlayer){
            if(![10, 12, 22, 24, 34, 36, 46, 48].includes((clickedCellIndex+1) + (clickedCellIndex-1))) {
            document.getElementById('alrt')!.innerHTML='<p>You Cannot place symbol here, only two in row is allowed in drop Faze</p>'; 
            setTimeout(function() {document.getElementById('alrt')!.innerHTML='';},6000);
            return
        }
        }
        if(gameState[clickedCellIndex+1] == currentPlayer && gameState[clickedCellIndex+2] == currentPlayer){
            if(![11, 23, 35, 47].includes((clickedCellIndex+1) + (clickedCellIndex+2))) {
            document.getElementById('alrt')!.innerHTML='<b>You Cannot place symbol here, only two in row is allowed in drop Faze</b>'; 
            setTimeout(function() {document.getElementById('alrt')!.innerHTML='';},6000);
            return
        }
        }
        if(gameState[clickedCellIndex-1] == currentPlayer && gameState[clickedCellIndex-2] == currentPlayer){
            if(![11, 23, 35, 47].includes((clickedCellIndex-1) + (clickedCellIndex-2))) {
            document.getElementById('alrt')!.innerHTML='<b>You Cannot place symbol here, only two in row is allowed in drop Faze</b>'; 
            setTimeout(function() {document.getElementById('alrt')!.innerHTML='';},6000);
            return
        }
        }
        if(gameState[clickedCellIndex+6] == currentPlayer && gameState[clickedCellIndex-6] == currentPlayer){
            document.getElementById('alrt')!.innerHTML='<p>You Cannot place symbol here, only two in row is allowed in drop Faze</p>'; 
            setTimeout(function() {document.getElementById('alrt')!.innerHTML='';},6000);
            return
        }
        if(gameState[clickedCellIndex+12] == currentPlayer && gameState[clickedCellIndex+6] == currentPlayer){
            document.getElementById('alrt')!.innerHTML='<b>You Cannot place symbol here, only two in row is allowed in drop Faze</b>'; 
            setTimeout(function() {document.getElementById('alrt')!.innerHTML='';},6000);
            return
        }
        if(gameState[clickedCellIndex-12] == currentPlayer && gameState[clickedCellIndex-6] == currentPlayer){
            document.getElementById('alrt')!.innerHTML='<b>You Cannot place symbol here, only two in row is allowed in drop Faze</b>'; 
            setTimeout(function() {document.getElementById('alrt')!.innerHTML='';},6000);
            return
        }
        
    if(computer == false){
        handleCellPlayed(clickedCell, clickedCellIndex);
        handlePlayerChange();
        dropFazeCount();
    }
    if(computer == true){
        let computerList = [];
        handleCellPlayed(clickedCell, clickedCellIndex);
        for (var i = 0; i < 30; i++) {
            if(gameState[i] == ""){
                computerList.push(i);
            }
        }
        const randomSpot = computerList[Math.floor(Math.random() * computerList.length)];
        gameState[randomSpot] = "O";
        let foo = randomSpot;
        let bar = '' + foo;
        var theDiv = document.getElementById(bar);
        theDiv!.innerHTML = "O";
        dropFazeCount();
    }
    }
    if(dropFaze == false && playerturn == 1){
        if(gameState[clickedCellIndex] !== ""){
            return
        }
        else{
            playerturn = 0;
            checkForThree(clickedCellEvent);
        }
        handleResultValidation();
    }

    else if(dropFaze == false && playerturn == 0) {
        takesDisplay!.innerHTML = playerTakes();
        if (gameState[clickedCellIndex] == "" || !gameActive) {
            return;
        }
        if(computer == false){
            if(currentPlayer == "X" && playerOneTake == 1){
                if(gameState[clickedCellIndex] == "O"){
                    moveAround(clickedCell, clickedCellIndex);
                    playerOneTake = 0;
                    takesDisplay!.innerHTML = playerTakes();
                    handlePlayerChange();
                }
                else{
                    return;
                }
            }
            if(currentPlayer == "O" && playerTwoTake == 1){
                if(gameState[clickedCellIndex] == "X"){
                    moveAround(clickedCell, clickedCellIndex);
                    playerTwoTake = 0;
                    takesDisplay!.innerHTML = playerTakes();
                    handlePlayerChange();
                }
                else{
                    return;
                }
            }
            else if(currentPlayer == "X" && gameState[clickedCellIndex] !== "" && playerOneTake == 0){
                if(gameState[clickedCellIndex] == "O" || gameState[clickedCellIndex] == ""){
                    return;
                }
                else{
                    moveAround(clickedCell, clickedCellIndex);
                    playerturn = 1;
                }
            }
            else if(currentPlayer == "O" && gameState[clickedCellIndex] !== "" && playerTwoTake == 0){
                if(gameState[clickedCellIndex] == "X" || gameState[clickedCellIndex] == ""){
                    return;
                }
                else{
                    moveAround(clickedCell, clickedCellIndex);
                    playerturn = 1;
                }
            }
        }
        else if (computer == true){
            if(gameState[clickedCellIndex] !== "" && playerOneTake == 0){
                if(gameState[clickedCellIndex] == "O"){
                    return;
                }
                else{
                    moveAround(clickedCell, clickedCellIndex);
                    playerturn = 1;
                }
            }
            if(currentPlayer == "X" && playerOneTake == 1){
                if(gameState[clickedCellIndex] == "O"){
                    moveAround(clickedCell, clickedCellIndex);
                    playerOneTake = 0;
                    takesDisplay!.innerHTML = playerTakes();
                }
                else{
                    return;
                }
            }

        }
        handleResultValidation();
    }
}

function checkForThree(clickedCellEvent: any){
        
    const clickedCell = clickedCellEvent.target;
    const clickedCellIndex = parseInt(clickedCell.getAttribute('id'));

    if(currentPlayer == "X"){
        currentPlayer = "X";
        currentindex = lastIndexOne;
    }
    if(currentPlayer == "O"){
        currentPlayer = "O";
        currentindex = lastIndexTwo;
    }
        if(gameState[clickedCellIndex+1] == "X" && gameState[clickedCellIndex-1] == "X" && currentPlayer == "X" && currentindex !== clickedCellIndex){
            if(![10, 12, 22, 24, 34, 36, 46, 48].includes((clickedCellIndex+1) + (clickedCellIndex-1))) {
                playerOneTake = 1;
                lastIndexOne = clickedCellIndex;
            }
        }
       else if(gameState[clickedCellIndex+1] == "X" && gameState[clickedCellIndex+2] == "X" && currentPlayer == "X" && currentindex !== clickedCellIndex){
            if(![11, 23, 35, 47].includes((clickedCellIndex+1) + (clickedCellIndex+2))) {
                playerOneTake = 1;
                lastIndexOne = clickedCellIndex;
            }
        }
        else if(gameState[clickedCellIndex-1] == "X" && gameState[clickedCellIndex-2] == "X" && currentPlayer == "X" && currentindex !== clickedCellIndex){
            if(![11, 23, 35, 47].includes((clickedCellIndex-1) + (clickedCellIndex-2))) {
                playerOneTake = 1;
                lastIndexOne = clickedCellIndex;
            }
        }
        else if(gameState[clickedCellIndex+1] == "O" && gameState[clickedCellIndex-1] == "O"  && currentPlayer == "O" && currentindex !== clickedCellIndex){
            if(![10, 11, 24, 36, 48].includes((clickedCellIndex+1) + (clickedCellIndex-1))) {
                playerTwoTake = 1;
                lastIndexTwo = clickedCellIndex;
            }
        }
        else if(gameState[clickedCellIndex+1] == "O" && gameState[clickedCellIndex+2] == "O" && currentPlayer == "O" && currentindex !== clickedCellIndex){
            if(![11, 23, 35, 47].includes((clickedCellIndex+1) + (clickedCellIndex+2))) {
                playerTwoTake = 1;
                lastIndexTwo = clickedCellIndex;
            }
        }
        else if(gameState[clickedCellIndex-1] == "O" && gameState[clickedCellIndex-2] == "O"  && currentPlayer == "O" && currentindex !== clickedCellIndex){
            if(![11, 23, 35, 47].includes((clickedCellIndex-1) + (clickedCellIndex-2))) {
                playerTwoTake = 1;
                lastIndexTwo = clickedCellIndex;
            }
        }

        else if(gameState[clickedCellIndex+6] == "X" && gameState[clickedCellIndex-6] == "X" && currentPlayer == "X" && currentindex !== clickedCellIndex){
            playerOneTake = 1;
            lastIndexOne = clickedCellIndex;
        }
        else if(gameState[clickedCellIndex+12] == "X" && gameState[clickedCellIndex+6] == "X" && currentPlayer == "X" && currentindex !== clickedCellIndex){
            playerOneTake = 1;
            lastIndexOne = clickedCellIndex;
        }
        else if(gameState[clickedCellIndex-12] == "X" && gameState[clickedCellIndex-6] == "X" && currentPlayer == "X" && currentindex !== clickedCellIndex){
            playerOneTake = 1;
            lastIndexOne = clickedCellIndex;
        }

        else if(gameState[clickedCellIndex+6] == "O" && gameState[clickedCellIndex-6] == "O" && currentPlayer == "O" && currentindex !== clickedCellIndex){
            playerTwoTake = 1;
            lastIndexTwo = clickedCellIndex;
        }
        else if(gameState[clickedCellIndex+12] == "O" && gameState[clickedCellIndex+6] == "O" && currentPlayer == "O" && currentindex !== clickedCellIndex){
            playerTwoTake = 1;
            lastIndexTwo = clickedCellIndex;
        }
        else if(gameState[clickedCellIndex-12] == "O" && gameState[clickedCellIndex-6] == "O" && currentPlayer == "O" && currentindex !== clickedCellIndex){
            playerTwoTake = 1;
            lastIndexTwo = clickedCellIndex;
        }
        gameState[clickedCellIndex] = currentPlayer;
        clickedCell.innerHTML = currentPlayer;

    if(gameState[clickedCellIndex] == "O"){
        lastIndexTwo = clickedCellIndex;
    }
    if(gameState[clickedCellIndex] == "X"){
        lastIndexOne = clickedCellIndex;
    }

    takesDisplay!.innerHTML = playerTakes();

    if(currentPlayer == "X" && playerOneTake == 1){
        return;
    }
    if(currentPlayer == "O" && playerTwoTake == 1){
        return;
    }
    else{
        if(computer == false){
            handlePlayerChange();
        }
        if(computer == true){
            let computerList2 = [];
            let computerList3 = [];
            let enemeyList = [];
            for (var i = 0; i < 30; i++) {
                if(gameState[i] == "O"){
                    computerList2.push(i);
                }
                if(gameState[i] == ""){
                    computerList3.push(i)
                }
                if(gameState[i] == "X"){
                    enemeyList.push(i)
                }
            }
            const randomSpot2 = computerList2[Math.floor(Math.random() * computerList2.length)];
            gameState[randomSpot2] = "";
            let foo2 = randomSpot2;
            let bar2 = '' + foo2;
            var theDiv = document.getElementById(bar2);
            theDiv!.innerHTML = "";

            const randomSpot3 = computerList3[Math.floor(Math.random() * computerList3.length)];
            gameState[randomSpot3] = "O";
            let foo3 = randomSpot3;
            let bar3 = '' + foo3;
            var theDiv = document.getElementById(bar3);
            theDiv!.innerHTML = "O";

            if(gameState[randomSpot3+1] == "O" && gameState[randomSpot3-1] == "O"){
                if(![10, 22, 34, 46].includes((randomSpot3+1) + (randomSpot3-1))) {
                    const enemySpotEliminate = enemeyList[Math.floor(Math.random() * enemeyList.length)];
                    gameState[enemySpotEliminate] = "";
                    let enemy = enemySpotEliminate;
                    let enemyBar = '' + enemy;
                    var theDiv = document.getElementById(enemyBar);
                    theDiv!.innerHTML = "";
                }
            }
            else if(gameState[randomSpot3+1] == "O" && gameState[randomSpot3+2] == "O"){
                if(![11, 23, 35, 47].includes((randomSpot3+1) + (randomSpot3+2))) {
                    const enemySpotEliminate = enemeyList[Math.floor(Math.random() * enemeyList.length)];
                    gameState[enemySpotEliminate] = "";
                    let enemy = enemySpotEliminate;
                    let enemyBar = '' + enemy;
                    var theDiv = document.getElementById(enemyBar);
                    theDiv!.innerHTML = "";
                }
            }
            else if(gameState[randomSpot3-1] == "O" && gameState[randomSpot3-2] == "O"){
                if(![11, 23, 35, 47].includes((i-1) + (i-2))) {
                    const enemySpotEliminate = enemeyList[Math.floor(Math.random() * enemeyList.length)];
                    gameState[enemySpotEliminate] = "";
                    let enemy = enemySpotEliminate;
                    let enemyBar = '' + enemy;
                    var theDiv = document.getElementById(enemyBar);
                    theDiv!.innerHTML = "";
                }
            }
            else if(gameState[randomSpot3+6] == "O" && gameState[randomSpot3-6] == "O"){
                const enemySpotEliminate = enemeyList[Math.floor(Math.random() * enemeyList.length)];
                gameState[enemySpotEliminate] = "";
                let enemy = enemySpotEliminate;
                let enemyBar = '' + enemy;
                var theDiv = document.getElementById(enemyBar);
                theDiv!.innerHTML = "";
            }
            else if(gameState[randomSpot3+12] == "O" && gameState[randomSpot3+6] == "O"){
                const enemySpotEliminate = enemeyList[Math.floor(Math.random() * enemeyList.length)];
                gameState[enemySpotEliminate] = "";
                let enemy = enemySpotEliminate;
                let enemyBar = '' + enemy;
                var theDiv = document.getElementById(enemyBar);
                theDiv!.innerHTML = "";
            }
            else if(gameState[randomSpot3-12] == "O" && gameState[randomSpot3-6] == "O"){
                const enemySpotEliminate = enemeyList[Math.floor(Math.random() * enemeyList.length)];
                gameState[enemySpotEliminate] = "";
                let enemy = enemySpotEliminate;
                let enemyBar = '' + enemy;
                var theDiv = document.getElementById(enemyBar);
                theDiv!.innerHTML = "";
            }    
        }
    }
}

function handleCellPlayed(clickedCell: any, clickedCellIndex: any) {
    gameState[clickedCellIndex] = currentPlayer;
    clickedCell.innerHTML = currentPlayer;
}

function handlePlayerChange() {
    currentPlayer = currentPlayer === "X" ? "O" : "X";
    statusDisplay!.innerHTML = currentPlayerTurn();
}
function handleResultValidation() {
    if (getAmount() !== "" && dropFaze == false) {
        statusDisplay!.innerHTML = winningMessage();
        gameActive = false;
        return;
    }
}

function handleRestartGame() {
    gameActive = true;
    currentPlayer = "X";
    gameState = ["", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""];
    statusDisplay!.innerHTML = currentPlayerTurn();
    document.querySelectorAll('.cell')
               .forEach(cell => cell.innerHTML = "");
    dropFaze = true;
    playerturn = 0;
    computer = false;
}

function computerVsHuman() {
    gameActive = true;
    currentPlayer = "X";
    gameState = ["", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""];
    statusDisplay!.innerHTML = currentPlayerTurn();
    document.querySelectorAll('.cell')
               .forEach(cell => cell.innerHTML = "");
    dropFaze = true;
    playerturn = 0;
    computer = true;
}

function AI() {
    gameActive = true;
    currentPlayer = "X";
    gameState = ["", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ""];
    statusDisplay!.innerHTML = currentPlayerTurn();
    document.querySelectorAll('.cell')
               .forEach(cell => cell.innerHTML = "");
    dropFaze = true;
    playerturn = 0;
    computerVsComputer = true;
    AIvsAI();
}

function dropFazeCount(){
    let count = 0;
    for (var i = 0; i <= 30; i++) {
        if(gameState[i] == "X" || (gameState[i] == "O" )){
            count++;
        }
    }
    if(count == 24){
        dropFaze = false;
        console.log("dropzone is false");
    }
}

function moveAround(clickedCell: any, clickedCellIndex: any){
    gameState[clickedCellIndex] = "";
    clickedCell.innerHTML = "";
}
function getAmount(){
    let countPlayerOne = 0;
    let countPlayerTwo = 0;
    for (var i = 0; i < 30; i++) {
        if(gameState[i] == "X"){
            countPlayerOne++;
        }
        else if(gameState[i] == "O"){
            countPlayerTwo++;
        }
    }
    if(countPlayerOne <= 2){
        currentPlayer = "X";
        winner = "Player Two"
        return "Two";
    }
    else if(countPlayerTwo <= 2){
        currentPlayer = "O";
        winner = "Player One"
        return "One";
    }
    else{
        return "";
    }
}

function AIvsAI(){
    if(dropFaze == true){
        let computerList = [];
        for (var i = 0; i < 30; i++) {
            if(gameState[i] == ""){
                computerList.push(i);
            }
        }
        const randomSpot = computerList[Math.floor(Math.random() * computerList.length)];
        gameState[randomSpot] = "X";
        let foo = randomSpot;
        let bar = '' + foo;
        var theDiv = document.getElementById(bar);
        theDiv!.innerHTML = "X";

        currentPlayer = "O"
        statusDisplay!.innerHTML = currentPlayerTurn();

        setTimeout(function(){
        let computerList2 = [];
        for (var i = 0; i < 30; i++) {
            if(gameState[i] == ""){
                computerList2.push(i);
            }
        }
        const randomSpot2 = computerList2[Math.floor(Math.random() * computerList2.length)];
        gameState[randomSpot2] = "O";
        foo = randomSpot2;
        bar = '' + foo;
        var theDiv2 = document.getElementById(bar);
        theDiv2!.innerHTML = "O";
        dropFazeCount();
        }, 1000)
        setTimeout(function(){
            AIvsAI();
            currentPlayer = "X"
            statusDisplay!.innerHTML = currentPlayerTurn();
        }, 1000)
    }
    if(dropFaze == false){
        let computerList = [];
        let computerList2 = [];
        let enemeyList = [];
        for (var i = 0; i < 30; i++) {
            if(gameState[i] == "X"){
                computerList.push(i);
            }
            if(gameState[i] == ""){
                computerList2.push(i);
            }
            if(gameState[i] == "O"){
                enemeyList.push(i)
            }
        }
        const randomSpot = computerList[Math.floor(Math.random() * computerList.length)];
        gameState[randomSpot] = "";
        let foo = randomSpot;
        let bar = '' + foo;
        var theDiv = document.getElementById(bar);
        theDiv!.innerHTML = "";

        const randomSpot2 = computerList2[Math.floor(Math.random() * computerList2.length)];
        gameState[randomSpot2] = "X";
        foo = randomSpot2;
        bar = '' + foo;
        var theDiv2 = document.getElementById(bar);
        theDiv2!.innerHTML = "X";

        if(gameState[randomSpot2+1] == "X" && gameState[randomSpot2-1] == "X"){
            if(![10, 22, 34, 46].includes((randomSpot2+1) + (randomSpot2-1))) {
                const enemySpotEliminate = enemeyList[Math.floor(Math.random() * enemeyList.length)];
                gameState[enemySpotEliminate] = "";
                let enemy = enemySpotEliminate;
                let enemyBar = '' + enemy;
                var theDiv = document.getElementById(enemyBar);
                theDiv!.innerHTML = "";
            }
        }
        else if(gameState[randomSpot2+1] == "X" && gameState[randomSpot2+2] == "X"){
            if(![11, 23, 35, 47].includes((randomSpot2+1) + (randomSpot2+2))) {
                const enemySpotEliminate = enemeyList[Math.floor(Math.random() * enemeyList.length)];
                gameState[enemySpotEliminate] = "";
                let enemy = enemySpotEliminate;
                let enemyBar = '' + enemy;
                var theDiv = document.getElementById(enemyBar);
                theDiv!.innerHTML = "";
            }
        }
        else if(gameState[randomSpot2-1] == "X" && gameState[randomSpot2-2] == "X"){
            if(![11, 23, 35, 47].includes((i-1) + (i-2))) {
                const enemySpotEliminate = enemeyList[Math.floor(Math.random() * enemeyList.length)];
                gameState[enemySpotEliminate] = "";
                let enemy = enemySpotEliminate;
                let enemyBar = '' + enemy;
                var theDiv = document.getElementById(enemyBar);
                theDiv!.innerHTML = "";
            }
        }
        else if(gameState[randomSpot2+6] == "X" && gameState[randomSpot2-6] == "X"){
            const enemySpotEliminate = enemeyList[Math.floor(Math.random() * enemeyList.length)];
            gameState[enemySpotEliminate] = "";
            let enemy = enemySpotEliminate;
            let enemyBar = '' + enemy;
            var theDiv = document.getElementById(enemyBar);
            theDiv!.innerHTML = "";
        }
        else if(gameState[randomSpot2+12] == "X" && gameState[randomSpot2+6] == "X"){
            const enemySpotEliminate = enemeyList[Math.floor(Math.random() * enemeyList.length)];
            gameState[enemySpotEliminate] = "";
            let enemy = enemySpotEliminate;
            let enemyBar = '' + enemy;
            var theDiv = document.getElementById(enemyBar);
            theDiv!.innerHTML = "";
        }
        else if(gameState[randomSpot2-12] == "X" && gameState[randomSpot2-6] == "X"){
            const enemySpotEliminate = enemeyList[Math.floor(Math.random() * enemeyList.length)];
            gameState[enemySpotEliminate] = "";
            let enemy = enemySpotEliminate;
            let enemyBar = '' + enemy;
            var theDiv = document.getElementById(enemyBar);
            theDiv!.innerHTML = "";
        }
        handleResultValidation();
        //Second AI Move
        computerList2 = [];
        for (var i = 0; i < 30; i++) {
            if(gameState[i] == "O"){
                computerList2.push(i);
            }
        }
        const randomSpot3 = computerList2[Math.floor(Math.random() * computerList2.length)];
        gameState[randomSpot3] = "";
        foo = randomSpot3;
        bar = '' + foo;
        var theDiv2 = document.getElementById(bar);
        theDiv2!.innerHTML = "";
    
        let computerList3 = [];
        for (var i = 0; i < 30; i++) {
            if(gameState[i] == ""){
                computerList3.push(i);
            }
        }
        const randomSpot4 = computerList3[Math.floor(Math.random() * computerList3.length)];
        gameState[randomSpot4] = "O";
        foo = randomSpot4;
        bar = '' + foo;
        var theDiv3 = document.getElementById(bar);
        theDiv3!.innerHTML = "O";


        if(gameState[randomSpot4+1] == "O" && gameState[randomSpot4-1] == "O"){
            if(![10, 22, 34, 46].includes((randomSpot4+1) + (randomSpot4-1))) {
                const enemySpotEliminate = enemeyList[Math.floor(Math.random() * enemeyList.length)];
                gameState[enemySpotEliminate] = "";
                let enemy = enemySpotEliminate;
                let enemyBar = '' + enemy;
                var theDiv = document.getElementById(enemyBar);
                theDiv!.innerHTML = "";
            }
        }
        else if(gameState[randomSpot4+1] == "O" && gameState[randomSpot4+2] == "O"){
            if(![11, 23, 35, 47].includes((randomSpot4+1) + (randomSpot4+2))) {
                const enemySpotEliminate = enemeyList[Math.floor(Math.random() * enemeyList.length)];
                gameState[enemySpotEliminate] = "";
                let enemy = enemySpotEliminate;
                let enemyBar = '' + enemy;
                var theDiv = document.getElementById(enemyBar);
                theDiv!.innerHTML = "";
            }
        }
        else if(gameState[randomSpot4-1] == "O" && gameState[randomSpot4-2] == "O"){
            if(![11, 23, 35, 47].includes((i-1) + (i-2))) {
                const enemySpotEliminate = enemeyList[Math.floor(Math.random() * enemeyList.length)];
                gameState[enemySpotEliminate] = "";
                let enemy = enemySpotEliminate;
                let enemyBar = '' + enemy;
                var theDiv = document.getElementById(enemyBar);
                theDiv!.innerHTML = "";
            }
        }
        else if(gameState[randomSpot4+6] == "O" && gameState[randomSpot4-6] == "O"){
            const enemySpotEliminate = enemeyList[Math.floor(Math.random() * enemeyList.length)];
            gameState[enemySpotEliminate] = "";
            let enemy = enemySpotEliminate;
            let enemyBar = '' + enemy;
            var theDiv = document.getElementById(enemyBar);
            theDiv!.innerHTML = "";
        }
        else if(gameState[randomSpot4+12] == "O" && gameState[randomSpot4+6] == "O"){
            const enemySpotEliminate = enemeyList[Math.floor(Math.random() * enemeyList.length)];
            gameState[enemySpotEliminate] = "";
            let enemy = enemySpotEliminate;
            let enemyBar = '' + enemy;
            var theDiv = document.getElementById(enemyBar);
            theDiv!.innerHTML = "";
        }
        else if(gameState[randomSpot4-12] == "O" && gameState[randomSpot4-6] == "O"){
            const enemySpotEliminate = enemeyList[Math.floor(Math.random() * enemeyList.length)];
            gameState[enemySpotEliminate] = "";
            let enemy = enemySpotEliminate;
            let enemyBar = '' + enemy;
            var theDiv = document.getElementById(enemyBar);
            theDiv!.innerHTML = "";
        }
        handleResultValidation();
        setTimeout(function(){
            AIvsAI();
        }, 1000)
    }
}

export { winner };
export { handleCellClick };
export { handleRestartGame};
export { computerVsHuman };
export { AI };