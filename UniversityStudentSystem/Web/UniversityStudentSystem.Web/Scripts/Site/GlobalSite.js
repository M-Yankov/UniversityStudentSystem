(function ($) {

    // disable body scroll. http://getbootstrap.com/javascript/#collapse - scroll down to events

    $(document.body).on('hide.bs.collapse', '.navbar-collapse', function (e) {
        if ($(e.target).is('div.sidebar-nav.navbar-collapse.collapse')) {
            $(document.body).css('overflow', ''); // enables scrolling
        }
    })

    $(document.body).on('show.bs.collapse', '.navbar-collapse', function (e) {
        if ($(e.target).is('div.sidebar-nav.navbar-collapse.collapse')) {
            $(document.body).css('overflow', 'hidden'); // disables scrolling
        }
    })

})(jQuery)