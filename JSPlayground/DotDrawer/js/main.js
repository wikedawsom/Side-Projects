document.dotColor = 'red';
document.dotRadius = 40;

function placeDot(event) {
    let mainWin = document.getElementById('main-window');
    let newDot = document.createElement('div');
    newDot.setAttribute('style','position: absolute;'+
                                'width:'+document.dotRadius+'px;'+
                                'height:'+document.dotRadius+'px;'+
                                'top:'+(event.offsetY- Math.round(document.dotRadius/4))+'px;'+
                                'left:'+(event.offsetX - Math.round(document.dotRadius/4))+'px;'+
                                'border-radius: 50%;'+
                                'background-color:'+document.dotColor+';');
    newDot.setAttribute('class','dot');
    
    mainWin.appendChild(newDot);
}
// Wait for full window to load before running script
document.addEventListener('DOMContentLoaded', (event) => {
    let mainWin = document.getElementById('main-window');
    mainWin.addEventListener('click', (event) => {
        placeDot(event);
        event.preventDefault();
        event.stopPropagation();
        let x = event.offsetX + 30/4;
    })
})