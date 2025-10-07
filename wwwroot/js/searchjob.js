// /* ===============================================================
// // =======================search for job================
// ===============================================================*/

document.addEventListener('DOMContentLoaded', function() {
    // Parse search query from URL parameter
    const urlParams = new URLSearchParams(window.location.search);
    const query = urlParams.get('query') ? urlParams.get('query').toLowerCase().trim() : '';

    // Get all elements with specific IDs and classes
    const elements = document.querySelectorAll(".col");

    // Iterate through elements and hide/show based on search query
    for (let element of elements) {
        const title = element.querySelector("#card-title").innerText.toLowerCase().trim();
        const category = element.querySelector("#categoryName").innerText.toLowerCase().trim();

        if (title.includes(query) || category.includes(query)) {
            element.style.display = "block"; // Show parent div with class "col"
        } else {
            element.style.display = "none";  // Hide parent div with class "col"
        }
    }
});


// /*============================
// // ======filter===== 
// ============================== */

document.addEventListener('DOMContentLoaded', function () {
    const checkboxes = document.querySelectorAll('.form-check-input');
    const colDivs = document.querySelectorAll('.col');
    const resultsContainer = document.getElementById('list-of-job');
    const searchInput = document.getElementById('searchInput');

    // Add event listeners for checkboxes and search input
    checkboxes.forEach(checkbox => {
        checkbox.addEventListener('change', updateResults);
    });
    searchInput.addEventListener('input', updateResults);

    function updateResults() {
        const query = searchInput.value.toLowerCase().trim();
        const selectedValues = Array.from(checkboxes)
            .filter(checkbox => checkbox.checked)
            .map(checkbox => checkbox.value.toLowerCase());

        resultsContainer.innerHTML = ''; // Clear previous results

        colDivs.forEach(colDiv => {
            const titleText = colDiv.querySelector("#card-title").innerText.toLowerCase().trim();
            const categoryText = colDiv.querySelector("#categoryName").innerText.toLowerCase().trim();

            const matchesSearch = titleText.includes(query) || categoryText.includes(query);
            const matchesFilter = selectedValues.length === 0 || selectedValues.includes(titleText) || selectedValues.includes(categoryText);

            if (matchesSearch && matchesFilter) {
                resultsContainer.appendChild(colDiv.cloneNode(true));
            }
        });

        if (resultsContainer.children.length === 0) {
            resultsContainer.textContent = 'No matching results found.';
        }
    }
});

/*====================================
// ---------------save job-------------
======================================*/
document.addEventListener('DOMContentLoaded', () => {
    // استمع لحدث النقر على علامة الحفظ في كل عنصر
    const bookmarkIcons = document.querySelectorAll('#bookmarkIcon');
    bookmarkIcons.forEach(bookmarkIcon => {
        // تحقق من حالة الأيقونة المحفوظة عند التحميل
        const savedSections = JSON.parse(localStorage.getItem('savedSections')) || [];
        const currentSection = bookmarkIcon.closest('.col').outerHTML;
        const isSaved = savedSections.some(section => section.html === currentSection);

        if (isSaved) {
            bookmarkIcon.classList.add('saved');
        }

        bookmarkIcon.addEventListener('click', function() {
            const savedSections = JSON.parse(localStorage.getItem('savedSections')) || [];
            const currentSection = this.closest('.col').outerHTML;

            const existingIndex = savedSections.findIndex(section => section.html === currentSection);

            if (existingIndex !== -1) {
                // إذا تم العثور على القسم المحفوظ، قم بحذفه
                savedSections.splice(existingIndex, 1);
                this.classList.remove('saved'); // إزالة النمط المحفوظ
            } else {
                // إذا لم يتم العثور على القسم، قم بإضافته
                savedSections.push({ html: currentSection });
                this.classList.add('saved'); // إضافة النمط المحفوظ
            }

            // حفظ الأقسام المحدثة في local storage
            localStorage.setItem('savedSections', JSON.stringify(savedSections));

            // إعادة تحميل الصفحة لتحديث عرض السكشنات
            location.reload();
        });
    });
});

// ####################################




