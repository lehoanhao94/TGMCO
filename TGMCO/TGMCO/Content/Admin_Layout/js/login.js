
var form = $('.form');
var btn = $('#submit');
var topbar = $('.topbar');
var input = $('#password');
var input2 = $('#username');
var article =$('.article');
var tries = 0;
var h = input.height();
$('.spanColor').height(h+23);
$('#findpass').on('click',function(){
  $(this).text('this-is-soo-cool');
});
input.on('focus',function(){
  topbar.removeClass('error success');
  input.text('');
});
btn.on('click',function(){
  if(tries<=2){
    var pass = $('#password').val();
    setTimeout(function(){
      btn.text('Success!');
    },250);
    topbar.addClass('Đang đăng nhập');
    form.addClass('goAway');
    article.addClass('active');
    tries=0;
  }
  else{
    topbar.addClass('disabled');
  }
  
});

$('.form').keypress(function(e){
   if(e.keyCode==13)
   submit.click();
});
input.keypress(function(){
  topbar.removeClass('success error');
});