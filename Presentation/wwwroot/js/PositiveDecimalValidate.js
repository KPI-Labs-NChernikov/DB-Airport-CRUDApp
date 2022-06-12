jQuery.validator.addMethod("positive-decimal",
    function (value, element, param) {
        if (value > 0) {
            return true;
        }
        else {
            return false;
        }
    });

jQuery.validator.unobtrusive.adapters.addBool("positive-decimal");