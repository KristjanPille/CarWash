import { handleCellClick, handleRestartGame, computerVsHuman, AI } from "./engine";


document.querySelectorAll('.cell').forEach(cell => cell.addEventListener('click', handleCellClick));
document.querySelector('.game--restart')!.addEventListener('click', handleRestartGame);
document.querySelector('.game--computer')!.addEventListener('click', computerVsHuman);
document.querySelector('.game--computerVsComputer')!.addEventListener('click', AI);



