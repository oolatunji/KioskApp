function genericFormValidation() {
    var invalidControls = '';
    var counter = 0;
    var errMessage = '';
    $('form input, form select').each(
     function (index) {
         var control = $(this);
         if (control.attr('required') == 'required' && control.val() == "") {
             invalidControls += control.attr('title') + ', ';
             counter++;
         } else if (control.attr('required') == 'required' && control.val() != "") {
             if (control.attr('type') == 'email') {
                 if (!validEmail(control.val())) {
                     invalidControls += 'A Valid ' + control.attr('title') + ', ';
                     counter++;
                 }
             }
             if (control.attr('type') == 'number') {
                 if (inValidNumber(control.val())) {
                     invalidControls += 'A Valid ' + control.attr('title') + ', ';
                     counter++;
                 }
             }             
         }
     }
    );
    invalidControls = _.trimRight(invalidControls, ', ');
    if (counter == 1)
        errMessage = 'The field ' + invalidControls + ' is required.';
    else if (counter > 1)
        errMessage = 'The following fields: ' + invalidControls + ' are required.';
    return errMessage;
}

function validEmail(email) {
    var re = /^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;
    return re.test(email);
}

function inValidNumber(number) {
    return isNaN(number);
}