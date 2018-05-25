var Login = function() {
    var i;    
    var handleLogin = function() {

        $('.login-form').validate({
            errorElement: 'span', //default input error message container
            errorClass: 'help-block', // default input error message class
            focusInvalid: false, // do not focus the last invalid input
            rules: {
                username: {
                    required: true
                },
                password: {
                    required: true
                },
                remember: {
                    required: false
                }
            },

            messages: {
                username: {
                    required: "Username is required."
                },
                password: {
                    required: "Password is required."
                }
            },

            invalidHandler: function(event, validator) { //display error alert on form submit                  
                //"请输入用户名和密码进行登陆."
                $("#msg").html("请输入用户名和密码进行登陆.");
                $('.alert-danger', $('.login-form')).show();
                
            },
            highlight: function (element) { // hightlight error inputs
                $(element)
                    .closest('.form-group').addClass('has-error'); // set error class to the control group
            },

            success: function(label) {
                label.closest('.form-group').removeClass('has-error');
                label.remove();
            },

            errorPlacement: function(error, element) {
                error.insertAfter(element.closest('.input-icon'));
            },

            submitHandler: function(form) {
                //orm.submit(); // form validation success, call ajax form submit
                msgDivHide();
                i = layer.load(); //Layer加载层              
                var input_username = $("input[name='username']");
                var input_password = $("input[name='password']");
                if (!isNull(input_username.val())||
                    !isNull(input_password.val())) {
                    $.ajax({
                        type: "post",
                        url: "/Login/Login",
                        data: { username: input_username.val(), password: input_password.val() },
                        dataType: "json",
                        success: function (response) {                           
                            if (response.State) {
                                layer.close(i);
                                msgalert('success', response.msg)
                                location.href="/Home/Index"
                                //msgDivShow(response.msg);
                            } else {
                                
                                layer.close(i);                           
                                //msgalert('error', response.msg)
                                msgDivShow(response.msg);
                            }
                           
                        },
                        error: function (XMLHttpRequest,textStatus,errorThrown) {
                            layer.close(i);
                            //msgalert("error", "网络错误请重试！")
                            msgDivShow("网络错误请重试！")
                        }
                    })
                } else {
                    layer.close(i);
                    msgDivShow("用户名或密码为空,请键入用户名和密码重新登录!");
                    //msgalert('error', "用户名或密码为空,请键入用户名和密码重新登录!")
                    //layer.msg("用户名或密码为空,请键入用户名和密码重新登录!");
                }
            }
        });

        $('.login-form input').keypress(function(e) {
            if (e.which == 13) {
                if ($('.login-form').validate().form()) {
                    $('.login-form').submit(); //form validation success, call ajax form submit
                }
                return false;
            }
        });       
        
        $(".login-form input").focus(function () {
            //alert("asdasda");
            msgDivHide();
        })
        
    }

    
    

    return {
        //main function to initiate the module
        init: function() {

            handleLogin();           
            // init background slide images 左侧轮播图
            //$('.login-bg').backstretch([
            //    "../assets/pages/img/login/bg1.jpg",
            //    "../assets/pages/img/login/bg2.jpg",
            //    "../assets/pages/img/login/bg3.jpg"
            //    ], {
            //      fade: 1000,
            //      duration: 8000
            //    }
            //);

            

        }

    };

}();

jQuery(document).ready(function() {
    Login.init();
});
function isNull(str) {
    if (str == "") return true;
    var regu = "^[ ]+$";
    var re = new RegExp(regu);
    return re.test(str);
}

function msgalert(toastType,msg) {
    // toastType:success绿,warning黄,info蓝,error红
    Command: toastr[toastType](msg)

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "positionClass": "toast-top-center",
        "onclick": null,
        "showDuration": "1000",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
}

function msgDivShow(msg) {
   
    $("#msg").html(msg);
    return $('.alert-danger').show();
}
function msgDivHide() {
    return $('.alert-danger').hide();
}