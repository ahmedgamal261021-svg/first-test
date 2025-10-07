jQuery(document).ready(function() {
	// click on next button
	jQuery('.form-wizard-next-btn').click(function() {
		var parentFieldset = jQuery(this).parents('.wizard-fieldset');
		var currentActiveStep = jQuery(this).parents('.form-wizard').find('.form-wizard-steps .active');
		var next = jQuery(this);
		var nextWizardStep = true;
		parentFieldset.find('.wizard-required').each(function(){
			var thisValue = jQuery(this).val();

			if( thisValue == "") {
				jQuery(this).siblings(".wizard-form-error").slideDown();
				nextWizardStep = false;
			}
			else {
				jQuery(this).siblings(".wizard-form-error").slideUp();
			}
		});
		if( nextWizardStep) {
			next.parents('.wizard-fieldset').removeClass("show","400");
			currentActiveStep.removeClass('active').addClass('activated').next().addClass('active',"400");
			next.parents('.wizard-fieldset').next('.wizard-fieldset').addClass("show","400");
			jQuery(document).find('.wizard-fieldset').each(function(){
				if(jQuery(this).hasClass('show')){
					var formAtrr = jQuery(this).attr('data-tab-content');
					jQuery(document).find('.form-wizard-steps .form-wizard-step-item').each(function(){
						if(jQuery(this).attr('data-attr') == formAtrr){
							jQuery(this).addClass('active');
							var innerWidth = jQuery(this).innerWidth();
							var position = jQuery(this).position();
							jQuery(document).find('.form-wizard-step-move').css({"left": position.left, "width": innerWidth});
						}else{
							jQuery(this).removeClass('active');
						}
					});
				}
			});
		}
	});
	//click on previous button
	jQuery('.form-wizard-previous-btn').click(function() {
		var counter = parseInt(jQuery(".wizard-counter").text());;
		var prev =jQuery(this);
		var currentActiveStep = jQuery(this).parents('.form-wizard').find('.form-wizard-steps .active');
		prev.parents('.wizard-fieldset').removeClass("show","400");
		prev.parents('.wizard-fieldset').prev('.wizard-fieldset').addClass("show","400");
		currentActiveStep.removeClass('active').prev().removeClass('activated').addClass('active',"400");
		jQuery(document).find('.wizard-fieldset').each(function(){
			if(jQuery(this).hasClass('show')){
				var formAtrr = jQuery(this).attr('data-tab-content');
				jQuery(document).find('.form-wizard-steps .form-wizard-step-item').each(function(){
					if(jQuery(this).attr('data-attr') == formAtrr){
						jQuery(this).addClass('active');
						var innerWidth = jQuery(this).innerWidth();
						var position = jQuery(this).position();
						jQuery(document).find('.form-wizard-step-move').css({"left": position.left, "width": innerWidth});
					}else{
						jQuery(this).removeClass('active');
					}
				});
			}
		});
	});
	//click on form submit button
	jQuery(document).on("click",".form-wizard .form-wizard-submit" , function(){
		var parentFieldset = jQuery(this).parents('.wizard-fieldset');
		var currentActiveStep = jQuery(this).parents('.form-wizard').find('.form-wizard-steps .active');
		parentFieldset.find('.wizard-required').each(function() {
			var thisValue = jQuery(this).val();
			if( thisValue == "" ) {
				jQuery(this).siblings(".wizard-form-error").slideDown();
			}
			else {
				jQuery(this).siblings(".wizard-form-error").slideUp();
			}
		});
	});
	// focus on input field check empty or not
	jQuery(".form-control").on('focus', function(){
		var tmpThis = jQuery(this).val();
		if(tmpThis == '' ) {
			jQuery(this).parent().addClass("focus-input");
		}
		else if(tmpThis !='' ){
			jQuery(this).parent().addClass("focus-input ");
		}
	}).on('blur', function(){
		var tmpThis = jQuery(this).val();
		if(tmpThis == '' ) {
			jQuery(this).parent().removeClass("focus-input");
			jQuery(this).siblings('.wizard-form-error').slideDown("3000");
		}
		else if(tmpThis !='' ){
			jQuery(this).parent().addClass("focus-input");
			jQuery(this).siblings('.wizard-form-error').slideUp("3000");
		}
	});
});
//country and goverment
function updateCityList() {
    const countrySelect = document.getElementById("countrySelect");
    const citySelect = document.getElementById("Governorate");
  
    // Get the selected country value and convert it to uppercase
    const selectedCountry = countrySelect.value.toUpperCase();
  
    // Clear the current city options
    citySelect.innerHTML = "";
    
    const cities = {
        MR: ["Nouakchott, Mauritania", "Nouadhibou, Mauritania", "Chicago","Kaedi, Mauritania","Rosso, Mauritania","Zouerat, Mauritania","Adel Bagrou, Mauritania"],
        DZ: [ "Oran ","Setif","Batna"," Constantine","Tlemcen", "Ghardaia" ,"Djemila "],
        SY: [ "Latakia","Hama","Raqqa","Hasakah","Tartous","Idlib","Palmyra","Bosra","Qamishli"],
        TN: [ "Sidi Bou Said ","Kairouan","Tunis","Maktar","Sbeitla","Bulla Regia","El Jem Amphitheater"],
        SD: [ " Abekr","Abushneib","Abyei","Al Fashir","Al Managil","Al Qadarif","Atbara","Babanusa","Berber","Buwaidhaa","Delgo","Dongola","Ad-Damazin","En Nahud","El-Obeid","Foro Baranga","Geneina"],
        QA: [ "Al Khor ","Al rayyan","Al Wakrah","Umm Salal","Doha","Al Shamal","Dukhan"],
        OM: [ " Adam As Sib",  "Al Ashkharah ","Al Buraimi", "Al Hamra ","Al Jazer"," Al Madina"," A Zarqa ","l Suwaiq Bahla"],
        MA: [ "Rabat ","Casablanca","Essaouira","Agadir","Meknes","Fes"," Chefchaouen"],
        LY: [ " Benghazi", "Tripoli", "Ghadames", "Misrata", "Cyrene", "Leptis", "Magna", "Sabratha", "Sirte", "Tobruk"],
        LB: [ " Benghazi", "Tripoli", "Ghadames" ,"Misrata" ,"Cyrene" ,"Leptis" ,"Magna" ,"Sabratha", "Sirte Tobruk"],
        KW: [ "Al Jahrā’ ","Abū Ḩulayfah" ,"Al Aḩmadī" ,"Ar Riqqah" ,"Al Bida" ,"Al Farwānīyah" ,"Abraq Khayţān" ,"Al ‘Udaylīyah" ,"Al ‘Abbāsīyah", "Ash Shadādīyah" ,"An Nijfah" ,"Al Quşūr Mishrif" ,"Al Massīlah "],
        EG: ["Cairo" ," Alexandria ","GovernorateAswan" ,"Asyut" ,"Beheira" ,"Beni Suef" ,"Dakahlia ","Damietta" ,"Fayoum" ,"Gharbia" , "Giza"  ,"Ismailia"  ,"Kafr El Sheikh" ,"Luxor"  ,"New Valley"  ,"Matrouh"  ,"Minya"  ,"Monufia"  ,"Qalyubia"  ,"Red" ,"Sea Port" ,"Said  North Sinai"  ,"Suez  "],
       
      // Add more cities as needed
    };
    // Populate the city options based on the selected country
    cities[selectedCountry].forEach(city => {
      const option = document.createElement("option");
      option.value = city;
      option.textContent = city;
      citySelect.appendChild(option);
    });
};
//eye password
function togglePassword() {
    var passwordInput = document.getElementById("password");
    var toggleIcon = document.querySelector(".toggle-password");

    if (passwordInput.type === "password") {
      passwordInput.type = "text";
      toggleIcon.textContent = "🙉";
    } else {
      passwordInput.type = "password";
      toggleIcon.textContent = "🙈";
    }
  }
  //check Age
  function checkAge(event) {
    const ageInput = event.target;
    const ageValue = parseInt(ageInput.value, 10);
  
    if (ageValue < 18 ^ ageValue >50) {
      ageInput.setCustomValidity("The age must be greater than or equal to 18.");
      document.getElementById("message").innerText = "The age must be greater than or equal to 18 and less than 50 ❎";
      message.style.color = "red";
     } else {
      ageInput.setCustomValidity("");
      document.getElementById("message").innerText = "Suitable age ✅";
      message.style.color = "green";
    }
  }
  
  //check password
  function checkPasswordStrength(event) {
    const passwordInput = event.target;
    const passwordValue = passwordInput.value;
  
    const hasNumber = /\d/.test(passwordValue);
    const hasUppercase = /[A-Z]/.test(passwordValue);
    const hasLowercase = /[a-z]/.test(passwordValue);
    const hasSpecial = /[!@#$%^&*(),.?":{}|<>]/.test(passwordValue);
    const length = passwordValue.length;
  
    let strength = 0;
  
    if (length > 8) {
      strength += 1;
    }
  
    if (hasNumber) {
      strength += 1;
    }
  
    if (hasUppercase) {
      strength += 1;
    }
  
    if (hasLowercase) {
      strength += 1;
    }
  
    if (hasSpecial) {
      strength += 1;
    }
  
    const bars = document.querySelectorAll(".password-strength .bar");
  
    bars[0].style.width = (strength === 0 ? "20%" : strength === 1 ? "40%" : "0%");
    bars[1].style.width = (strength === 2 ? "40%" : strength === 3 ? "60%" : "0%");
    bars[2].style.width = (strength === 4 ? "80%" : "0%");
  
    let message = "";
  
    switch (strength) {
      case 0:
        message = "Very weak";
        break;
      case 1:
        message = "Weak";
        break;
      case 2:
        message = "Moderate";
        break;
      case 3:
        message = "Strong";
        break;
      case 4:
        message = "Very strong";
        break;
      default:
        message = "";
    }
  
    document.getElementById("password-message-text").innerText = `Add ${8 + strength} characters or more, lowercase letters, uppercase letters, numbers and symbols to make the password really strong!`;
  
    document.getElementById("password-submit").disabled = (strength < 4);
  }
  //check email
  function validateEmail() {
    const email = document.getElementById("emailInput").value;
    const regex = /^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$/;
    const message = document.getElementById("message");

    if (regex.test(email)) {
        message.innerHTML = " is Valid email ✅";
        message.style.color = "green";
    } else {
        message.innerHTML = " Invalid email ❎";
        message.style.color = "red";
    }
}
