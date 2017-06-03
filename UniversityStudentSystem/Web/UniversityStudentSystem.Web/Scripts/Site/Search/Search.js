(function ($) {

    $('body').on('submit', '#search-form', function (e) {

        var countOfSelectedTypes = $('#search-form input[type=checkbox]:checked').length;
        if (!countOfSelectedTypes) {
            e.preventDefault();
            $('#error-message').removeClass('hidden');
        }
    })

    $('body').on('change', 'input[type=checkbox]', function (e) {

        $('#error-message').addClass('hidden');

        // All check-box
        if ($(e.target).is('#All')) {
            var checkboxes = $('#search-form input[type=checkbox]');
            for (var i = 0; i < checkboxes.length; i++) {
                checkboxes[i].checked = true;
            }

            return;
        }

        var countOfSelectedTypes = $('#search-form input[type=checkbox]:checked:not("#All")').length;
        if (countOfSelectedTypes === 5) {
            $('#All')[0].checked = true;
        } else {
            $('#All')[0].checked = false;
        }
    });

})(jQuery)