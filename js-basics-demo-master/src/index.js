import { handleCellClick, handleRestartGame, computerVsHuman } from "./engine.js";


document.querySelectorAll('.cell').forEach(cell => cell.addEventListener('click', handleCellClick));
document.querySelector('.game--restart').addEventListener('click', handleRestartGame);
document.querySelector('.game--computer').addEventListener('click', computerVsHuman);



