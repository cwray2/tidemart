// https://codepen.io/gulam-ahmad-siddiqui/pen/KKzaWmO
//I did modify this code as some of it was not working correctly

setInterval(displayClock,1000);
function displayClock(){
var d = new Date();
var hours = d.getHours();
var minutes = d.getMinutes();
var seconds = d.getSeconds();
var am_pm = 'AM';
 
if (hours > 12){
    hours = hours - 12;
    am_pm = 'PM';
} 
if(hours == 0){
   hours = 12;
}
if(hours < 10 ){
    hours = '0' + hours; 
} 
if(minutes < 10 ){
    minutes = '0' + minutes; 
}
if(seconds < 10 ){
    seconds = '0' + seconds; 
}
if (hours >= 12){
    am_pm = 'PM'; 
}
document.getElementById('clock').innerHTML= hours + " : " + minutes + " : " + seconds;
}
