!function(e){var n={};function t(o){if(n[o])return n[o].exports;var l=n[o]={i:o,l:!1,exports:{}};return e[o].call(l.exports,l,l.exports,t),l.l=!0,l.exports}t.m=e,t.c=n,t.d=function(e,n,o){t.o(e,n)||Object.defineProperty(e,n,{enumerable:!0,get:o})},t.r=function(e){"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},t.t=function(e,n){if(1&n&&(e=t(e)),8&n)return e;if(4&n&&"object"==typeof e&&e&&e.__esModule)return e;var o=Object.create(null);if(t.r(o),Object.defineProperty(o,"default",{enumerable:!0,value:e}),2&n&&"string"!=typeof e)for(var l in e)t.d(o,l,function(n){return e[n]}.bind(null,l));return o},t.n=function(e){var n=e&&e.__esModule?function(){return e.default}:function(){return e};return t.d(n,"a",n),n},t.o=function(e,n){return Object.prototype.hasOwnProperty.call(e,n)},t.p="",t(t.s=0)}([function(e,n,t){"use strict";t.r(n);let o=["","","","","","","","","","","","","","","","","","","","","","","","","","","","","",""],l=0,r=0,i=0,c=0,u="",d=!1,a=0,s=!0,f=!0,m="X",g=0,M=!1;const h=()=>`${m}'s turn`,y=()=>`Player one´s takes: ${l} | Player two´s takes: ${r}`,O=document.querySelector(".game--status"),T=document.querySelector(".takes");function L(e){const n=e.target,t=parseInt(n.getAttribute("id"));if(1==f&&0==M){if(""!==o[t]||!s)return;if(o[t+1]==m&&o[t-1]==m&&![10,12,22,24,34,36,46,48].includes(t+1+(t-1)))return document.getElementById("alrt").innerHTML="<p>You Cannot place symbol here, only two in row is allowed in drop Faze</p>",void setTimeout((function(){document.getElementById("alrt").innerHTML=""}),6e3);if(o[t+1]==m&&o[t+2]==m&&![11,23,35,47].includes(t+1+(t+2)))return document.getElementById("alrt").innerHTML="<b>You Cannot place symbol here, only two in row is allowed in drop Faze</b>",void setTimeout((function(){document.getElementById("alrt").innerHTML=""}),6e3);if(o[t-1]==m&&o[t-2]==m&&![11,23,35,47].includes(t-1+(t-2)))return document.getElementById("alrt").innerHTML="<b>You Cannot place symbol here, only two in row is allowed in drop Faze</b>",void setTimeout((function(){document.getElementById("alrt").innerHTML=""}),6e3);if(o[t+6]==m&&o[t-6]==m)return document.getElementById("alrt").innerHTML="<p>You Cannot place symbol here, only two in row is allowed in drop Faze</p>",void setTimeout((function(){document.getElementById("alrt").innerHTML=""}),6e3);if(o[t+12]==m&&o[t+6]==m)return document.getElementById("alrt").innerHTML="<b>You Cannot place symbol here, only two in row is allowed in drop Faze</b>",void setTimeout((function(){document.getElementById("alrt").innerHTML=""}),6e3);if(o[t-12]==m&&o[t-6]==m)return document.getElementById("alrt").innerHTML="<b>You Cannot place symbol here, only two in row is allowed in drop Faze</b>",void setTimeout((function(){document.getElementById("alrt").innerHTML=""}),6e3);if(0==d&&(H(n,t),X(),E()),1==d){let e=[];H(n,t);for(var u=0;u<30;u++)""==o[u]&&e.push(u);const l=e[Math.floor(Math.random()*e.length)];o[l]="O";let r=""+l;document.getElementById(r).innerHTML="O",E()}}if(0==f&&1==g){if(""!==o[t])return;g=0,function(e){const n=e.target,t=parseInt(n.getAttribute("id"));"X"==m&&(m="X",a=i);"O"==m&&(m="O",a=c);console.log(o),"X"==o[t+1]&&"X"==o[t-1]&&"X"==m&&a!==t?[10,12,22,24,34,36,46,48].includes(t+1+(t-1))||(console.log("1"),l=1,i=t):"X"==o[t+1]&&"X"==o[t+2]&&"X"==m&&a!==t?[11,23,35,47].includes(t+1+(t+2))||(console.log("2"),l=1,i=t):"X"==o[t-1]&&"X"==o[t-2]&&"X"==m&&a!==t?[11,23,35,47].includes(t-1+(t-2))||(console.log("3"),l=1,i=t):"O"==o[t+1]&&"O"==o[t-1]&&"O"==m&&a!==t?[10,11,24,36,48].includes(t+1+(t-1))||(console.log("4"),r=1,c=t):"O"==o[t+1]&&"O"==o[t+2]&&"O"==m&&a!==t?[11,23,35,47].includes(t+1+(t+2))||(console.log("5"),r=1,c=t):"O"==o[t-1]&&"O"==o[t-2]&&"O"==m&&a!==t?[11,23,35,47].includes(t-1+(t-2))||(console.log("6"),r=1,c=t):"X"==o[t+6]&&"X"==o[t-6]&&"X"==m&&a!==t?(l=1,i=t):"X"==o[t+12]&&"X"==o[t+6]&&"X"==m&&a!==t?(l=1,i=t):"X"==o[t-12]&&"X"==o[t-6]&&"X"==m&&a!==t?(l=1,i=t):"O"==o[t+6]&&"O"==o[t-6]&&"O"==m&&a!==t?(r=1,c=t):"O"==o[t+12]&&"O"==o[t+6]&&"O"==m&&a!==t?(r=1,c=t):"O"==o[t-12]&&"O"==o[t-6]&&"O"==m&&a!==t&&(r=1,c=t);o[t]=m,n.innerHTML=m,"O"==o[t]&&(c=t);"X"==o[t]&&(i=t);if(T.innerHTML=y(),"X"==m&&1==l)return;if("O"==m&&1==r)return;if(0==d&&X(),1==d){let e=[],n=[],t=[];for(var u=0;u<30;u++)"O"==o[u]&&e.push(u),""==o[u]&&n.push(u),"X"==o[u]&&t.push(u);const l=e[Math.floor(Math.random()*e.length)];o[l]="";let r=""+l;document.getElementById(r).innerHTML="";const i=n[Math.floor(Math.random()*n.length)];o[i]="O";let c=""+i;if(document.getElementById(c).innerHTML="O","O"==o[i+1]&&"O"==o[i-1]){if(![10,22,34,46].includes(i+1+(i-1))){console.log(i);const e=t[Math.floor(Math.random()*t.length)];o[e]="";let n=""+e;document.getElementById(n).innerHTML=""}}else if("O"==o[i+1]&&"O"==o[i+2]){if(![11,23,35,47].includes(i+1+(i+2))){console.log(i);const e=t[Math.floor(Math.random()*t.length)];o[e]="";let n=""+e;document.getElementById(n).innerHTML=""}}else if("O"==o[i-1]&&"O"==o[i-2]){if(![11,23,35,47].includes(u-1+(u-2))){console.log(i);const e=t[Math.floor(Math.random()*t.length)];o[e]="";let n=""+e;document.getElementById(n).innerHTML=""}}else if("O"==o[i+6]&&"O"==o[i-6]){console.log(u);const e=t[Math.floor(Math.random()*t.length)];o[e]="";let n=""+e;document.getElementById(n).innerHTML=""}else if("O"==o[i+12]&&"O"==o[i+6]){console.log(u);const e=t[Math.floor(Math.random()*t.length)];o[e]="";let n=""+e;document.getElementById(n).innerHTML=""}else if("O"==o[i-12]&&"O"==o[i-6]){console.log(u);const e=t[Math.floor(Math.random()*t.length)];o[e]="";let n=""+e;document.getElementById(n).innerHTML=""}}}(e),p()}else if(0==f&&0==g){if(T.innerHTML=y(),""==o[t]||!s)return;if(0==d){if("X"==m&&1==l){if("O"!=o[t])return;I(n,t),l=0,T.innerHTML=y(),X()}if("O"==m&&1==r){if("X"!=o[t])return;I(n,t),r=0,T.innerHTML=y(),X()}else if("X"==m&&""!==o[t]&&0==l){if("O"==o[t]||""==o[t])return;console.log("Now move it to another place"),I(n,t),g=1}else if("O"==m&&""!==o[t]&&0==r){if("X"==o[t]||""==o[t])return;console.log("Now move it to another place"),I(n,t),g=1}}else if(1==d){if(""!==o[t]&&0==l){if("O"==o[t])return;console.log("Now move it to another place"),I(n,t),g=1}if("X"==m&&1==l){if("O"!=o[t])return;I(n,t),l=0,T.innerHTML=y()}}p()}}function H(e,n){o[n]=m,e.innerHTML=m}function X(){m="X"===m?"O":"X",O.innerHTML=h()}function p(){if(""!==function(){console.log(o);let e=0,n=0;for(var t=0;t<30;t++)"X"==o[t]?(console.log("Player x lose"),e++):"O"==o[t]&&(n++,console.log("Player y lose"));return e<=2?(console.log(e),console.log("ESIMENE"),m="X",u="Player Two","Two"):n<=2?(console.log(n),console.log("TEINE"),m="O",u="Player One","One"):""}()&&0==f)return O.innerHTML=`${u} has won!`,void(s=!1)}function E(){let e=0;for(var n=0;n<=30;n++)"X"!=o[n]&&"O"!=o[n]||e++;24==e&&(f=!1,console.log("dropzone is false"))}function I(e,n){o[n]="",e.innerHTML=""}document.querySelectorAll(".cell").forEach(e=>e.addEventListener("click",L)),document.querySelector(".game--restart").addEventListener("click",(function(){s=!0,m="X",o=["","","","","","","","","","","","","","","","","","","","","","","","","","","","","",""],O.innerHTML=h(),document.querySelectorAll(".cell").forEach(e=>e.innerHTML=""),f=!0,g=0,d=!1})),document.querySelector(".game--computer").addEventListener("click",(function(){s=!0,m="X",o=["","","","","","","","","","","","","","","","","","","","","","","","","","","","","",""],O.innerHTML=h(),document.querySelectorAll(".cell").forEach(e=>e.innerHTML=""),f=!0,g=0,d=!0})),document.querySelector(".game--computerVsComputer").addEventListener("click",(function(){s=!0,m="X",o=["","","","","","","","","","","","","","","","","","","","","","","","","","","","","",""],O.innerHTML=h(),document.querySelectorAll(".cell").forEach(e=>e.innerHTML=""),f=!0,g=0,M=!0,function e(){if(1==f){let t=[];for(var n=0;n<30;n++)""==o[n]&&t.push(n);const l=t[Math.floor(Math.random()*t.length)];o[l]="X";let r=l,i=""+r;document.getElementById(i).innerHTML="X",m="O",O.innerHTML=h(),setTimeout((function(){let e=[];for(var n=0;n<30;n++)""==o[n]&&e.push(n);const t=e[Math.floor(Math.random()*e.length)];o[t]="O",r=t,i=""+r,document.getElementById(i).innerHTML="O",E()}),1e3),setTimeout((function(){e(),m="X",O.innerHTML=h()}),1e3)}if(0==f){let t=[],l=[],r=[];for(n=0;n<30;n++)"X"==o[n]&&t.push(n),""==o[n]&&l.push(n),"O"==o[n]&&r.push(n);const i=t[Math.floor(Math.random()*t.length)];o[i]="";let c=i,u=""+c;document.getElementById(u).innerHTML="";const d=l[Math.floor(Math.random()*l.length)];if(o[d]="X",c=d,u=""+c,document.getElementById(u).innerHTML="X","X"==o[d+1]&&"X"==o[d-1]){if(![10,22,34,46].includes(d+1+(d-1))){console.log(d);const e=r[Math.floor(Math.random()*r.length)];o[e]="";let n=""+e;document.getElementById(n).innerHTML=""}}else if("X"==o[d+1]&&"X"==o[d+2]){if(![11,23,35,47].includes(d+1+(d+2))){console.log(d);const e=r[Math.floor(Math.random()*r.length)];o[e]="";let n=""+e;document.getElementById(n).innerHTML=""}}else if("X"==o[d-1]&&"X"==o[d-2]){if(![11,23,35,47].includes(n-1+(n-2))){console.log(d);const e=r[Math.floor(Math.random()*r.length)];o[e]="";let n=""+e;document.getElementById(n).innerHTML=""}}else if("X"==o[d+6]&&"X"==o[d-6]){console.log(n);const e=r[Math.floor(Math.random()*r.length)];o[e]="";let t=""+e;document.getElementById(t).innerHTML=""}else if("X"==o[d+12]&&"X"==o[d+6]){console.log(n);const e=r[Math.floor(Math.random()*r.length)];o[e]="";let t=""+e;document.getElementById(t).innerHTML=""}else if("X"==o[d-12]&&"X"==o[d-6]){console.log(n);const e=r[Math.floor(Math.random()*r.length)];o[e]="";let t=""+e;document.getElementById(t).innerHTML=""}p(),l=[];for(n=0;n<30;n++)"O"==o[n]&&l.push(n);const a=l[Math.floor(Math.random()*l.length)];o[a]="",c=a,u=""+c,document.getElementById(u).innerHTML="";let s=[];for(n=0;n<30;n++)""==o[n]&&s.push(n);const f=s[Math.floor(Math.random()*s.length)];if(o[f]="O",c=f,u=""+c,document.getElementById(u).innerHTML="O","O"==o[f+1]&&"O"==o[f-1]){if(![10,22,34,46].includes(f+1+(f-1))){console.log(f);const e=r[Math.floor(Math.random()*r.length)];o[e]="";let n=""+e;document.getElementById(n).innerHTML=""}}else if("O"==o[f+1]&&"O"==o[f+2]){if(![11,23,35,47].includes(f+1+(f+2))){console.log(f);const e=r[Math.floor(Math.random()*r.length)];o[e]="";let n=""+e;document.getElementById(n).innerHTML=""}}else if("O"==o[f-1]&&"O"==o[f-2]){if(![11,23,35,47].includes(n-1+(n-2))){console.log(f);const e=r[Math.floor(Math.random()*r.length)];o[e]="";let n=""+e;document.getElementById(n).innerHTML=""}}else if("O"==o[f+6]&&"O"==o[f-6]){console.log(n);const e=r[Math.floor(Math.random()*r.length)];o[e]="";let t=""+e;document.getElementById(t).innerHTML=""}else if("O"==o[f+12]&&"O"==o[f+6]){console.log(n);const e=r[Math.floor(Math.random()*r.length)];o[e]="";let t=""+e;document.getElementById(t).innerHTML=""}else if("O"==o[f-12]&&"O"==o[f-6]){console.log(n);const e=r[Math.floor(Math.random()*r.length)];o[e]="";let t=""+e;document.getElementById(t).innerHTML=""}p(),setTimeout((function(){e()}),1e3)}}()}))}]);