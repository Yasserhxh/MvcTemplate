$(window).on("load", function () {

  var wind = $(window);


  // stellar
  wind.stellar();


  // isotope
  $('.gallery').isotope({
    // options
    itemSelector: '.items'
  });

  var $gallery = $('.gallery').isotope({
    // options
  });


  // filter items on button click
  $('.filtering').on('click', 'span', function () {

    var filterValue = $(this).attr('data-filter');

    $gallery.isotope({
      filter: filterValue
    });


  });

  $('.filtering').on('click', 'span', function () {

    $(this).addClass('active').siblings().removeClass('active');

  });


  $gallery.isotope({
    filter: '.viennoiseries'
  });

});
