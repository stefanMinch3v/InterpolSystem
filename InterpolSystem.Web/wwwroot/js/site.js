// Write your JavaScript code.
let result;
$(document).ready(function () {
    let element = $('<p class="glyphicon glyphicon-hand-down"></p>');

    $("input, textarea").on('focus', function () {
        element.insertBefore($(this));
    });
    $("input, textarea").on('blur', function () {
        element.remove();
    });

    //animation for navbar
    $('.navbar .dropdown').hover(function () {
        $(this).find('.dropdown-menu').first().stop(true, true).slideToggle(400);
    }, function () {
        $(this).find('.dropdown-menu').first().stop(true, true).slideToggle(400)
        });

    //notification about submit forms
    result = $('.result').text();
    $('.result').css('display', 'none');
    if (result !=="") {
        let li = $("#SubmitFormWantedA");
        let span = $(`<span>&nbsp${result}&nbsp</span >`)
            .css({
                'background-color': 'red',
                'border-radius': '50%',
                'text-align': 'right',
                'margin-left': '2px',
                'margin-right': '2px'
            })
            .appendTo(li);

        let ul = $("#managePeopleA");
        let span2 = $(`<span>&nbsp${result} </span >`)
            .css({
                'background-color': 'red',
                'border-radius': '50%',
                'text-align': 'right',
                'margin-left': '2px',
                'margin-right': '2px'
            })
            .prependTo(ul);
    }

    // right nav bar - articles
    let aElement = $('.list-group-item>a');
    aElement.css('text-decoration', 'none');

    aElement.on('mouseover', function () {
        $(this).fadeOut(300);
        $(this).fadeIn(600);
        $(this).addClass('text-danger');
        $(this).removeClass('glyphicon glyphicon-hand-right');
        $(this).css('font-size', '170%');
    });

    aElement.on('mouseout', function () {
        $(this).addClass('glyphicon glyphicon-hand-right');
        $(this).removeClass('text-danger');
        $(this).css('font-size', '100%');
    });
});
