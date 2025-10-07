
(function() {
  "use strict";

  /**
   * Easy selector helper function
   */
  const select = (el, all = false) => {
    el = el.trim()
    if (all) {
      return [...document.querySelectorAll(el)]
    } else {
      return document.querySelector(el)
    }
  }

  /**
   * Easy event listener function
   */
  const on = (type, el, listener, all = false) => {
    let selectEl = select(el, all)
    if (selectEl) {
      if (all) {
        selectEl.forEach(e => e.addEventListener(type, listener))
      } else {
        selectEl.addEventListener(type, listener)
      }
    }
  }

  /**
   * Easy on scroll event listener 
   */
  const onscroll = (el, listener) => {
    el.addEventListener('scroll', listener)
  }

  /**
   * Navbar links active state on scroll
   */
  let navbarlinks = select('#navbar .scrollto', true)
  const navbarlinksActive = () => {
    let position = window.scrollY + 200
    navbarlinks.forEach(navbarlink => {
      if (!navbarlink.hash) return
      let section = select(navbarlink.hash)
      if (!section) return
      if (position >= section.offsetTop && position <= (section.offsetTop + section.offsetHeight)) {
        navbarlink.classList.add('active')
      } else {
        navbarlink.classList.remove('active')
      }
    })
  }
  window.addEventListener('load', navbarlinksActive)
  onscroll(document, navbarlinksActive)

  /**
   * Scrolls to an element with header offset
   */
  const scrollto = (el) => {
    let header = select('#header')
    let offset = header.offsetHeight

    if (!header.classList.contains('header-scrolled')) {
      offset -= 16
    }

    let elementPos = select(el).offsetTop
    window.scrollTo({
      top: elementPos - offset,
      behavior: 'smooth'
    })
  }

  /**
   * Toggle .header-scrolled class to #header when page is scrolled
   */
  let selectHeader = select('#header')
  if (selectHeader) {
    const headerScrolled = () => {
      if (window.scrollY > 100) {
        selectHeader.classList.add('header-scrolled')
      } else {
        selectHeader.classList.remove('header-scrolled')
      }
    }
    window.addEventListener('load', headerScrolled)
    onscroll(document, headerScrolled)
  }

  /**
   * Back to top button
   */
  let backtotop = select('.back-to-top')
  if (backtotop) {
    const toggleBacktotop = () => {
      if (window.scrollY > 100) {
        backtotop.classList.add('active')
      } else {
        backtotop.classList.remove('active')
      }
    }
    window.addEventListener('load', toggleBacktotop)
    onscroll(document, toggleBacktotop)
  }

  /**
   * Mobile nav toggle
   */
  on('click', '.mobile-nav-toggle', function(e) {
    select('#navbar').classList.toggle('navbar-mobile')
    this.classList.toggle('bi-list')
    this.classList.toggle('bi-x')
  })

  /**
   * Mobile nav dropdowns activate
   */
  on('click', '.navbar .dropdown > a', function(e) {
    if (select('#navbar').classList.contains('navbar-mobile')) {
      e.preventDefault()
      this.nextElementSibling.classList.toggle('dropdown-active')
    }
  }, true)

  /**
   * Scrool with ofset on links with a class name .scrollto
   */
  on('click', '.scrollto', function(e) {
    if (select(this.hash)) {
      e.preventDefault()

      let navbar = select('#navbar')
      if (navbar.classList.contains('navbar-mobile')) {
        navbar.classList.remove('navbar-mobile')
        let navbarToggle = select('.mobile-nav-toggle')
        navbarToggle.classList.toggle('bi-list')
        navbarToggle.classList.toggle('bi-x')
      }
      scrollto(this.hash)
    }
  }, true)

  /**
   * Scroll with ofset on page load with hash links in the url
   */
  window.addEventListener('load', () => {
    if (window.location.hash) {
      if (select(window.location.hash)) {
        scrollto(window.location.hash)
      }
    }
  });

  /**
   * Porfolio isotope and filter
   */
  window.addEventListener('load', () => {
    let portfolioContainer = select('.portfolio-container');
    if (portfolioContainer) {
      let portfolioIsotope = new Isotope(portfolioContainer, {
        itemSelector: '.portfolio-item'
      });

      let portfolioFilters = select('#portfolio-flters li', true);

      on('click', '#portfolio-flters li', function(e) {
        e.preventDefault();
        portfolioFilters.forEach(function(el) {
          el.classList.remove('filter-active');
        });
        this.classList.add('filter-active');

        portfolioIsotope.arrange({
          filter: this.getAttribute('data-filter')
        });

      }, true);
    }

  });

  /**
   * Initiate portfolio lightbox 
   */
  // const portfolioLightbox = GLightbox({
  //   selector: '.portfolio-lightbox'
  // });

  /**
   * Portfolio details slider
   */
  // new Swiper('.portfolio-details-slider', {
  //   speed: 400,
  //   loop: true,
  //   autoplay: {
  //     delay: 5000,
  //     disableOnInteraction: false
  //   },
  //   pagination: {
  //     el: '.swiper-pagination',
  //     type: 'bullets',
  //     clickable: true
  //   }
  // });

  // /**
  //  * Initiate Pure Counter 
  //  */
  // new PureCounter();

})();




// show more jobs------------------------------------

function showMoreJobs(){
  var cols = document.getElementsByClassName('col');

    // Display the first 8 items
    for (var i = 0; i < 8; i++) {
      cols[i].style.display = 'block';
    }

    var currentIndex = 8;
    var itemsPerPage = 8;
    var showMoreButton = document.getElementById('button-addon2');

    showMoreButton.addEventListener('click', function() {
      // Show 8 more items when the button is clicked
      var endIndex = currentIndex + itemsPerPage;
      if (endIndex > cols.length) {
        endIndex = cols.length;
        showMoreButton.style.display = 'none'; // Hide the button if there are no more items
      }

      for (var i = currentIndex; i < endIndex; i++) {
        cols[i].style.display = 'block';
      }

      currentIndex += itemsPerPage;
    });
}showMoreJobs();
// End show more jobs------------------------------------



// =========search for job ======================================
function search() {
  const searchTerm = document.getElementById("searchInput").value.trim();
  
  if (searchTerm !== "") {
    // Redirect to results page with search query as URL parameter
    window.location.href = `searchJob.html?query=${encodeURIComponent(searchTerm)}`;
  } else {
    // Display all divs
    const allDivs = document.querySelectorAll('.col');
    allDivs.forEach(div => {
      div.style.display = 'block'; // Show the div
    });
  }
};

function searchAfterSign() {
  const searchAfter = document.getElementById("searchInput").value.trim();
  
  if (searchAfter !== "") {
    // Redirect to results page with search query as URL parameter
    window.location.href = `viewjop.php?query=${encodeURIComponent(searchAfter)}`;
  } else {
    // Display all divs
    const allDiv = document.querySelectorAll('.col');
    allDiv.forEach(divs => {
      divs.style.display = 'block'; // Show the div
    });
  }
};
// =========End search for job ======================================





// search category ===========

  document.addEventListener('DOMContentLoaded', function () {
    const categoryLinks = document.querySelectorAll('.categoryLink');
  
    categoryLinks.forEach(link => {
        link.addEventListener('click', function (event) {
            event.preventDefault();
            const searchText = this.textContent.trim().toLowerCase();
            displayMatchingDivs(searchText);
        });
    });
  
    function displayMatchingDivs(searchText) {
        const colDivs = document.querySelectorAll('.col');
  
        colDivs.forEach(div => {
            const cardTitles = div.querySelectorAll('#card-title');
            let containsSearchText = Array.from(cardTitles).some(cardTitle => {
                return cardTitle.textContent.trim().toLowerCase() === searchText;
            });
  
            if (containsSearchText) {
                div.style.display = 'block';
            } else {
                div.style.display = 'none';
            }
        });
    }

  });



// post job-----------------------------------------

document.addEventListener('DOMContentLoaded', function () {
  // Display form data on this page
  displayFormData();
});



// function displayFormData() {
//   const formData = JSON.parse(localStorage.getItem('formData'));
//   console.log("show job sucsess");
//   if (formData) {
//       // Display form data wherever needed on this page
//       document.getElementById('list-of-job').innerHTML =`
  
//           <div class="col"id="col">
//             <div class="card h-100">
//               <div class="pt">
//                 <div class="logo">
//                   <a href=""><img src="assets/img/services.png" class="card-img-top" alt="..."></a>
//                   <p class="card-title"id="card-title"><a href="">${formData.Jobname}</a></p>
//                 </div>
                
//                 <a href="#" class="bookmark-btn"><i class="bi bi-bookmark"></i></a>
                
//               </div>
//               <div class="card-body">
                
//                 <p class="card-text"id="card-title">${formData.details}</p>
//               </div>
//               <hr>
//               <div class="card-count">
//                 <div class="time">
//                   <p class="text-muted">Last updated 3 mins ago</p>
//                 </div>
//                 <div class="time"id="categoryName">
//                   <p class="text-muted"id="card-title">${formData.Jobfield}</p>
//                 </div>
//               </div>
//             </div>
//           </div>
//       `;
//   }
// };

//End post job-----------------------------------------


// function displayFormData() {
//   const formData = JSON.parse(localStorage.getItem('formData'));
//   const listOfJobElement = document.getElementById('list-of-job');
//   console.log(formData);
//   if (formData && listOfJobElement) {
//       // Display form data wherever needed on the current page
//       listOfJobElement.innerHTML = `
//           <div class="col">
//               <div class="card h-100">
//                   <div class="pt">
//                       <div class="logo">
//                           <a href=""><img src="assets/img/services.png" class="card-img-top" alt="..."></a>
//                           <p class="card-title"><a href="">${formData.Jobname}</a></p>
//                       </div>
//                       <a href="#" class="bookmark-btn"><i class="bi bi-bookmark"></i></a>
//                   </div>
//                   <div class="card-body">
//                       <p class="card-text">${formData.details}</p>
//                   </div>
//                   <hr>
//                   <div class="card-count">
//                       <div class="time">
//                           <p class="text-muted">Last updated 3 mins ago</p>
//                       </div>
//                       <div class="time" id="categoryName">
//                           <p class="text-muted">${formData.Jobfield}</p>
//                       </div>
//                   </div>
//               </div>
//           </div>
//       `;
//   } else {
//       console.log("Form data not found or 'list-of-job' element not found");
//   }
// };



