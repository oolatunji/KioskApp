/* JS */


/* Navigation */

$(document).ready(function(){
/*
  $(window).resize(function()
  {
    if($(window).width() >= 765){
      $(".sidebar #nav").slideDown(350);
    }
    else{
      $(".sidebar #nav").slideUp(350); 
    }
  }); */
  
   $(".has_sub > a").click(function(e){
    e.preventDefault();
    var menu_li = $(this).parent("li");
    var menu_ul = $(this).next("ul");

    if(menu_li.hasClass("open")){
      menu_ul.slideUp(350);
      menu_li.removeClass("open")
    }
    else{
      $("#nav > li > ul").slideUp(350);
      $("#nav > li").removeClass("open");
      menu_ul.slideDown(350);
      menu_li.addClass("open");
    }
  });

/* Old Code 

  $("#nav > li > a").on('click',function(e){
      if($(this).parent().hasClass("has_sub")) {
       
		  e.preventDefault();

		  if(!$(this).hasClass("subdrop")) {
			// hide any open menus and remove all other classes
			$("#nav li ul").slideUp(350);
			$("#nav li a").removeClass("subdrop");
			
			// open our new menu and add the open class
			$(this).next("ul").slideDown(350);
			$(this).addClass("subdrop");
		  }
		  
		  else if($(this).hasClass("subdrop")) {
			$(this).removeClass("subdrop");
			$(this).next("ul").slideUp(350);
		  } 
      }   
      
  }); */
});

$(document).ready(function () {
    $('#data-table-1').dataTable({
        "sPaginationType": "full_numbers"
    });
});


$(document).ready(function(){
  $(".sidebar-dropdown a").on('click',function(e){
      e.preventDefault();
      
      if(!$(this).hasClass("open")) {
        // open our new menu and add the open class
        $(".sidebar #nav").slideDown(350);
        $(this).addClass("open");
      }
      
      else{
        $(".sidebar #nav").slideUp(350);
        $(this).removeClass("open");
      }
  });
 
    /* Widget minimize */

  $('.wminimize').click(function (e) {
      e.preventDefault();
      var $wcontent = $(this).parent().parent().next('.widget-content');

      if ($wcontent.is(':visible')) {
          $(this).children('i').removeClass('fa fa-chevron-up');
          $(this).children('i').addClass('fa fa-chevron-down');
      }
      else {
          $(this).children('i').removeClass('fa fa-chevron-down');
          $(this).children('i').addClass('fa fa-chevron-up');
      }
      $wcontent.toggle(500);
  });

    /* Widget close */

  $('.wclose').click(function (e) {
      e.preventDefault();
      var $wbox = $(this).parent().parent().parent();
      $wbox.hide(100);
  });


  $(".totop").hide();

});



$(function(){
	$(window).scroll(function(){
	  if ($(this).scrollTop()>300)
	  {
		$('.totop').fadeIn();
	  } 
	  else
	  {
		$('.totop').fadeOut();
	  }
	});

	$('.totop a').click(function (e) {
	  e.preventDefault();
	  $('body,html').animate({scrollTop: 0}, 500);
	});

});

