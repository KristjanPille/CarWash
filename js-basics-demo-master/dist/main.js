!function(e){var n={};function t(r){if(n[r])return n[r].exports;var o=n[r]={i:r,l:!1,exports:{}};return e[r].call(o.exports,o,o.exports,t),o.l=!0,o.exports}t.m=e,t.c=n,t.d=function(e,n,r){t.o(e,n)||Object.defineProperty(e,n,{enumerable:!0,get:r})},t.r=function(e){"undefined"!=typeof Symbol&&Symbol.toStringTag&&Object.defineProperty(e,Symbol.toStringTag,{value:"Module"}),Object.defineProperty(e,"__esModule",{value:!0})},t.t=function(e,n){if(1&n&&(e=t(e)),8&n)return e;if(4&n&&"object"==typeof e&&e&&e.__esModule)return e;var r=Object.create(null);if(t.r(r),Object.defineProperty(r,"default",{enumerable:!0,value:e}),2&n&&"string"!=typeof e)for(var o in e)t.d(r,o,function(n){return e[n]}.bind(null,o));return r},t.n=function(e){var n=e&&e.__esModule?function(){return e.default}:function(){return e};return t.d(n,"a",n),n},t.o=function(e,n){return Object.prototype.hasOwnProperty.call(e,n)},t.p="",t(t.s=0)}([function(e,n){const t=()=>`It's ${s}'s turn`,r=()=>`Player one´s takes: ${u} | Player two´s takes: ${a}`,o=document.querySelector(".game--status"),i=document.querySelector(".takes");let l=["","","","","","","","","","","","","","","","","","","","","","","","","","","","","",""],u=0,a=0,c="";document.querySelectorAll(".cell").forEach(e=>e.addEventListener("click",T)),document.querySelector(".game--restart").addEventListener("click",(function(){d=!0,s="X",l=["","","","","","","","","","","","","","","","","","","","","","","","","","","","","",""],o.innerHTML=t(),document.querySelectorAll(".cell").forEach(e=>e.innerHTML=""),f=!0,m=0}));let d=!0,f=!0,s="X",m=0;function y(){s="X"===s?"O":"X",o.innerHTML=t()}function p(){if(1==function(){let e=0;for(var n=0;n<=30;n++)""!==l[n]&&e++;return 14==e}()&&0==f)return o.innerHTML=`Player ${s} has won!`,void(d=!1)}function T(e){const n=e.target,t=parseInt(n.getAttribute("data-cell-index"));if(1==f){if(""!==l[t]||!d)return;if(l[t+1]==s&&l[t-1]==s)return document.getElementById("alrt").innerHTML="<p>You Cannot place symbol here, only two in row is allowed in drop Faze</p>",void setTimeout((function(){document.getElementById("alrt").innerHTML=""}),6e3);if(l[t+1]==s&&l[t+2]==s)return document.getElementById("alrt").innerHTML="<b>You Cannot place symbol here, only two in row is allowed in drop Faze</b>",void setTimeout((function(){document.getElementById("alrt").innerHTML=""}),6e3);if(l[t-1]==s&&l[t-2]==s)return document.getElementById("alrt").innerHTML="<b>You Cannot place symbol here, only two in row is allowed in drop Faze</b>",void setTimeout((function(){document.getElementById("alrt").innerHTML=""}),6e3);if(l[t+6]==s&&l[t-12]==s)return document.getElementById("alrt").innerHTML="<p>You Cannot place symbol here, only two in row is allowed in drop Faze</p>",void setTimeout((function(){document.getElementById("alrt").innerHTML=""}),6e3);if(l[t+12]==s&&l[t+6]==s)return document.getElementById("alrt").innerHTML="<b>You Cannot place symbol here, only two in row is allowed in drop Faze</b>",void setTimeout((function(){document.getElementById("alrt").innerHTML=""}),6e3);if(l[t-12]==s&&l[t-6]==s)return document.getElementById("alrt").innerHTML="<b>You Cannot place symbol here, only two in row is allowed in drop Faze</b>",void setTimeout((function(){document.getElementById("alrt").innerHTML=""}),6e3);!function(e,n){l[n]=s,e.innerHTML=s}(n,t),p(),y(),function(){let e=0;for(var n=0;n<=30;n++)"X"!=l[n]&&"O"!=l[n]||e++;24==e&&(f=!1,console.log("dropzone is false"))}()}if(0==f&&1==m){if(""!==l[t])return;m=0,function(e){const n=e.target,t=parseInt(n.getAttribute("data-cell-index"));"X"==l[t+1]&&"X"==l[t-1]&&""==l[t]&&(u=1);"X"==l[t+1]&&"X"==l[t+2]&&""==l[t]&&(u=1);"X"==l[t-1]&&"X"==l[t-2]&&""==l[t]&&(u=1);"O"==l[t+1]&&"O"==l[t-1]&&""==l[t]&&(a=1);"O"==l[t+1]&&"O"==l[t+2]&&""==l[t]&&(a=1);"O"==l[t-1]&&"O"==l[t-2]&&""==l[t]&&(a=1);if(c==l[t])return;l[t]=s,n.innerHTML=s;if(c=s,i.innerHTML=r(),1==u)return;if(1==a)return;y()}(e)}else if(0==f&&0==m){if(i.innerHTML=r(),""==l[t]||!d)return;if(0==f&&"X"==s&&1==u){if("O"!=l[t])return;b(n,t),u=0,i.innerHTML=r(),y()}if(0==f&&"O"==s&&1==a){if("X"!=l[t])return;b(n,t),a=0,i.innerHTML=r(),y()}else if("X"==s&&""!==l[t]&&0==u){if("O"==l[t])return;console.log("Now move it to another place"),b(n,t),m=1}else if("O"==s&&""!==l[t]&&0==a){if("X"==l[t])return;console.log("Now move it to another place"),b(n,t),m=1}}}function b(e,n){l[n]="",e.innerHTML=""}}]);