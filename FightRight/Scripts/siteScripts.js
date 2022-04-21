/*!
 * Javascript stuff
 * 
 */

const { each } = require("jquery");





// Helper function to get an element's exact position
function getPosition(el) {
    var xPos = 0;
    var yPos = 0;

    while (el) {
        if (el.tagName == "BODY") {
            // deal with browser quirks with body/window/document and page scroll
            var xScroll = el.scrollLeft || document.documentElement.scrollLeft;
            var yScroll = el.scrollTop || document.documentElement.scrollTop;

            xPos += (el.offsetLeft - xScroll + el.clientLeft);
            yPos += (el.offsetTop - yScroll + el.clientTop);
        } else {
            // for all other non-BODY elements
            xPos += (el.offsetLeft - el.scrollLeft + el.clientLeft);
            yPos += (el.offsetTop - el.scrollTop + el.clientTop);
        }

        el = el.offsetParent;
    }
    return {
        x: xPos,
        y: yPos
    };
};




//Runs through things as slides
function SlideShow(index, className, showAnimation, hideAnimation, blnShiftUp) {
    var slides = document.getElementsByClassName(className);
    var slideLength = slides.length;
    var i = 0;
    var newSlideShowIndex = index;

    
    for (i = 0; i < slideLength; i++) {
        if (slides[i].classList.contains(showAnimation) == true) {
            slides[i].classList.remove(showAnimation);
        }
        if (slides[i].classList.contains(hideAnimation) == false) {
            slides[i].classList.add(hideAnimation);
        }

    }

    

    if (slides[newSlideShowIndex].classList.contains(showAnimation) == false) {
        slides[newSlideShowIndex].classList.add(showAnimation);
        if (slides[newSlideShowIndex].classList.contains(hideAnimation) == true) {
            slides[newSlideShowIndex].classList.remove(hideAnimation);
        }
    }

    if (blnShiftUp) newSlideShowIndex++;
    else newSlideShowIndex--;

    if (newSlideShowIndex >= slideLength) {
        newSlideShowIndex = 0;
    }
    else if (newSlideShowIndex < 0) {
        newSlideShowIndex = 0;
    }

    return newSlideShowIndex;
};







window.addEventListener('DOMContentLoaded', event => {

    // Toggle the side navigation
    const sidebarToggle = document.body.querySelector('#sidebarToggle');


    
    //

    if (sidebarToggle) {
        // Uncomment Below to persist sidebar toggle between refreshes
        // if (localStorage.getItem('sb|sidebar-toggle') === 'true') {
        //     document.body.classList.toggle('sb-sidenav-toggled');
        // }
        sidebarToggle.addEventListener('click', event => {
            event.preventDefault();
            document.body.classList.toggle('sb-sidenav-toggled');
            localStorage.setItem('sb|sidebar-toggle', document.body.classList.contains('sb-sidenav-toggled'));
        });
    }

    


    

});