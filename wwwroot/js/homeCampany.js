
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
    if (all) {
      select(el, all).forEach(e => e.addEventListener(type, listener))
    } else {
      select(el, all).addEventListener(type, listener)
    }
  }

  /**
   * Easy on scroll event listener 
   */
  const onscroll = (el, listener) => {
    el.addEventListener('scroll', listener)
  }

  /**
   * Sidebar toggle
   */
  if (select('.toggle-sidebar-btn')) {
    on('click', '.toggle-sidebar-btn', function(e) {
      select('body').classList.toggle('toggle-sidebar')
    })
  }

  /**
   * Search bar toggle
   */
  if (select('.search-bar-toggle')) {
    on('click', '.search-bar-toggle', function(e) {
      select('.search-bar').classList.toggle('search-bar-show')
    })
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
   * Initiate tooltips
   */
  var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'))
  var tooltipList = tooltipTriggerList.map(function(tooltipTriggerEl) {
    return new bootstrap.Tooltip(tooltipTriggerEl)
  })

  /**
   * Initiate quill editors
   */
  if (select('.quill-editor-default')) {
    new Quill('.quill-editor-default', {
      theme: 'snow'
    });
  }

  if (select('.quill-editor-bubble')) {
    new Quill('.quill-editor-bubble', {
      theme: 'bubble'
    });
  }

  if (select('.quill-editor-full')) {
    new Quill(".quill-editor-full", {
      modules: {
        toolbar: [
          [{
            font: []
          }, {
            size: []
          }],
          ["bold", "italic", "underline", "strike"],
          [{
              color: []
            },
            {
              background: []
            }
          ],
          [{
              script: "super"
            },
            {
              script: "sub"
            }
          ],
          [{
              list: "ordered"
            },
            {
              list: "bullet"
            },
            {
              indent: "-1"
            },
            {
              indent: "+1"
            }
          ],
          ["direction", {
            align: []
          }],
          ["link", "image", "video"],
          ["clean"]
        ]
      },
      theme: "snow"
    });
  }

  /**
   * Initiate TinyMCE Editor
   */
  const useDarkMode = window.matchMedia('(prefers-color-scheme: dark)').matches;
  const isSmallScreen = window.matchMedia('(max-width: 1023.5px)').matches;

  // tinymce.init({
  //   selector: 'textarea.tinymce-editor',
  //   plugins: 'preview importcss searchreplace autolink autosave save directionality code visualblocks visualchars fullscreen image link media template codesample table charmap pagebreak nonbreaking anchor insertdatetime advlist lists wordcount help charmap quickbars emoticons',
  //   editimage_cors_hosts: ['picsum.photos'],
  //   menubar: 'file edit view insert format tools table help',
  //   toolbar: 'undo redo | bold italic underline strikethrough | fontfamily fontsize blocks | alignleft aligncenter alignright alignjustify | outdent indent |  numlist bullist | forecolor backcolor removeformat | pagebreak | charmap emoticons | fullscreen  preview save print | insertfile image media template link anchor codesample | ltr rtl',
  //   toolbar_sticky: true,
  //   toolbar_sticky_offset: isSmallScreen ? 102 : 108,
  //   autosave_ask_before_unload: true,
  //   autosave_interval: '30s',
  //   autosave_prefix: '{path}{query}-{id}-',
  //   autosave_restore_when_empty: false,
  //   autosave_retention: '2m',
  //   image_advtab: true,
  //   link_list: [{
  //       title: 'My page 1',
  //       value: 'https://www.tiny.cloud'
  //     },
  //     {
  //       title: 'My page 2',
  //       value: 'http://www.moxiecode.com'
  //     }
  //   ],
  //   image_list: [{
  //       title: 'My page 1',
  //       value: 'https://www.tiny.cloud'
  //     },
  //     {
  //       title: 'My page 2',
  //       value: 'http://www.moxiecode.com'
  //     }
  //   ],
  //   image_class_list: [{
  //       title: 'None',
  //       value: ''
  //     },
  //     {
  //       title: 'Some class',
  //       value: 'class-name'
  //     }
  //   ],
  //   importcss_append: true,
  //   file_picker_callback: (callback, value, meta) => {
  //     /* Provide file and text for the link dialog */
  //     if (meta.filetype === 'file') {
  //       callback('https://www.google.com/logos/google.jpg', {
  //         text: 'My text'
  //       });
  //     }

  //     /* Provide image and alt text for the image dialog */
  //     if (meta.filetype === 'image') {
  //       callback('https://www.google.com/logos/google.jpg', {
  //         alt: 'My alt text'
  //       });
  //     }

  //     /* Provide alternative source and posted for the media dialog */
  //     if (meta.filetype === 'media') {
  //       callback('movie.mp4', {
  //         source2: 'alt.ogg',
  //         poster: 'https://www.google.com/logos/google.jpg'
  //       });
  //     }
  //   },
  //   templates: [{
  //       title: 'New Table',
  //       description: 'creates a new table',
  //       content: '<div class="mceTmpl"><table width="98%%"  border="0" cellspacing="0" cellpadding="0"><tr><th scope="col"> </th><th scope="col"> </th></tr><tr><td> </td><td> </td></tr></table></div>'
  //     },
  //     {
  //       title: 'Starting my story',
  //       description: 'A cure for writers block',
  //       content: 'Once upon a time...'
  //     },
  //     {
  //       title: 'New list with dates',
  //       description: 'New List with dates',
  //       content: '<div class="mceTmpl"><span class="cdate">cdate</span><br><span class="mdate">mdate</span><h2>My List</h2><ul><li></li><li></li></ul></div>'
  //     }
  //   ],
  //   template_cdate_format: '[Date Created (CDATE): %m/%d/%Y : %H:%M:%S]',
  //   template_mdate_format: '[Date Modified (MDATE): %m/%d/%Y : %H:%M:%S]',
  //   height: 600,
  //   image_caption: true,
  //   quickbars_selection_toolbar: 'bold italic | quicklink h2 h3 blockquote quickimage quicktable',
  //   noneditable_class: 'mceNonEditable',
  //   toolbar_mode: 'sliding',
  //   contextmenu: 'link image table',
  //   skin: useDarkMode ? 'oxide-dark' : 'oxide',
  //   content_css: useDarkMode ? 'dark' : 'default',
  //   content_style: 'body { font-family:Helvetica,Arial,sans-serif; font-size:16px }'
  // });

  /**
   * Initiate Bootstrap validation check
   */
  var needsValidation = document.querySelectorAll('.needs-validation')

  Array.prototype.slice.call(needsValidation)
    .forEach(function(form) {
      form.addEventListener('submit', function(event) {
        if (!form.checkValidity()) {
          event.preventDefault()
          event.stopPropagation()
        }

        form.classList.add('was-validated')
      }, false)
    })

  /**
   * Initiate Datatables
   */
  const datatables = select('.datatable', true)
  datatables.forEach(datatable => {
    new simpleDatatables.DataTable(datatable, {
      perPageSelect: [5, 10, 15, ["All", -1]],
      columns: [{
          select: 2,
          sortSequence: ["desc", "asc"]
        },
        {
          select: 3,
          sortSequence: ["desc"]
        },
        {
          select: 4,
          cellClass: "green",
          headerClass: "red"
        }
      ]
    });
  })

  /**
   * Autoresize echart charts
   */
  const mainContainer = select('#main');
  if (mainContainer) {
    setTimeout(() => {
      new ResizeObserver(function() {
        select('.echart', true).forEach(getEchart => {
          echarts.getInstanceByDom(getEchart).resize();
        })
      }).observe(mainContainer);
    }, 200);
  }
  
})();




// -------------payment ----------price plan--------------------------
function togglePayment(plan) {
  const pricingSections = document.querySelectorAll('[id^="pricing"]');
  const paymentSection = document.getElementById('payment');
  const container = document.getElementById('container');
  // Hide all pricing sections except the clicked one
  pricingSections.forEach(section => {
      if (section.getAttribute('id') !== `pricing-${plan}`) {
          section.style.display = 'none';
          container.style.display = 'none';
      }
  });


  // Show payment section
  paymentSection.style.display = 'block';
};

// add-New-Job-------------------------
function addNewJob() {
  
  const addNewJob = document.getElementById('add-job');
  const companyjob = document.getElementById('company-job');
  

  companyjob.style.display = 'none';

  addNewJob.style.display = 'block';
  
};


// job-details-------------------------
function jobdetails() {

  const company = document.getElementById('company-job');
  const Jobdetails = document.getElementById('job-details');

  company.style.display = 'none';
  
  Jobdetails.style.display = 'block';
};



//  show applicants ----------
function ShowApplicants() {
  const companys = document.getElementById('company-job');
  const showapp = document.getElementById('show-app');

  companys.style.display = 'none';

  showapp.style.display = 'block';
};


// personal-Profile---------
function personalProfile() {

  const profileperson = document.querySelector('profile-person');
  const profileapp = document.getElementById('show-app');

  
  profileapp.style.display = 'none';

  profileperson.style.display = 'block';
  
};



// Favorite-job-------------------------
    // // استرجاع الأقسام المحفوظة من local storage
    // const savedSections = JSON.parse(localStorage.getItem('savedSections')) || [];

    // // إضافة الأقسام المحفوظة إلى #sectionContainer في الصفحة الثانية
    // const sectionContainer = document.getElementById('sectionContainer');
    // savedSections.forEach(section => {
    //     sectionContainer.innerHTML += section.html;
    // });

    // // استمع لحدث النقر على علامة الحفظ في الأقسام المعروضة لحذفها
    // sectionContainer.addEventListener('click', function(event) {
    //     if (event.target.classList.contains('bookmarkIcon')) {
    //         const currentSection = event.target.closest('.col').outerHTML;
    //         const savedSections = JSON.parse(localStorage.getItem('savedSections')) || [];
    //         const existingIndex = savedSections.findIndex(section => section.html === currentSection);

    //         if (existingIndex !== -1) {
    //             // إذا تم العثور على القسم المحفوظ، قم بحذفه
    //             savedSections.splice(existingIndex, 1);
    //         }

    //         // حفظ الأقسام المحدثة في local storage
    //         localStorage.setItem('savedSections', JSON.stringify(savedSections));

    //         // إعادة تحميل الصفحة لتحديث عرض السكشنات
    //         location.reload();
    //     }
    // });



// post job-----------------------------------------
function storeAndDisplayData() {
  const formData = {
    Jobname: document.getElementById('Job-name').value,
    Jobfield: document.getElementById('Job-field').value,
    // countrySelect: document.getElementById('countrySelect').value,
    // Governorate: document.getElementById('Governorate').value,
    // times: document.getElementById('times').value,
    // Number: document.getElementById('Number').value,
    details: document.getElementById('details').value
    // Educational: document.getElementById('Educational').value,
    // Experience: document.getElementById('Experience').value,
    // radio1: document.getElementById('radio1').value,
    // radio2: document.getElementById('radio2').value,
    // radio3: document.getElementById('radio3').value,
    // Minimum: document.getElementById('Minimum').value,
    // Maximum: document.getElementById('Maximum').value
  };

  // Store form data in local storage
  localStorage.setItem('formData', JSON.stringify(formData));

  // Display form data on the current page
  displayFormData();

  console.log("Data stored and displayed successfully!");

};

function displayFormData() {
  const formData = JSON.parse(localStorage.getItem('formData'));
  if (formData) {
      // Display form data wherever needed on the current page
      document.getElementById('list-of-job').innerHTML= `
      
          <div class="col"id="col">
            <div class="card h-100">
              <div class="pt">
                <div class="logo">
                  <a href=""><img src="assets/img/services.png" class="card-img-top" alt="..."></a>
                  <p class="card-title"id="card-title"><a href="">${formData.Jobname}</a></p>
                </div>
                
                <a href="#" class="bookmark-btn"><i class="bi bi-bookmark"></i></a>
                
              </div>
              <div class="card-body">
                
                <p class="card-text"id="card-title">${formData.details}</p>
              </div>
              <hr>
              <div class="card-count">
                <div class="time">
                  <p class="text-muted">Last updated 3 mins ago</p>
                </div>
                <div class="time"id="categoryName">
                  <p class="text-muted"id="card-title">${formData.Jobfield}</p>
                </div>
              </div>
            </div>
          </div>
      `;
  }else{
    console.log("not found");
  }
  
};
// End post job-----------------------------------------


// function displayFormData() {
//   const formData = JSON.parse(localStorage.getItem('formData'));
//   const listOfJobElement = document.getElementById('list-of-job');

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
