$('#formData').validate({
    errorElement: 'span', //default input error message container
    errorClass: 'help-block help-block-error', // default input error message class
    focusInvalid: false, // do not focus the last invalid input
    rules: {
        Name: {
            required: true
        },
        Description: {
            required: true
        },
        ActionList: {
            required: true
        },


    },

    messages: {
        Name: {
            required: "请输入角色名"
        },
        Description: {
            required: "请输入描述"
        }, ActionList: {
            required: "请选择权限"
        },

    },

    success: function (label) {
        label
            .closest('.form-group').removeClass('has-error'); // set success class to the control group
    },

    highlight: function (element) { // hightlight error inputs
        $(element)
            .closest('.form-group').removeClass("has-success").addClass('has-error'); // set error class to the control group   
    },

    submitHandler: function () {   //表单提交句柄,为一回调函数，带一个参数：form   

        if ($('#formData').validate()) {
            $.ajax({
                type: 'post',
                url: '/Role/Add',

                data: $('#formData').serialize(),
                success: function (msg) {
                    if (msg.State == true) {
                        alert(msg.Message)
                    }
                    else {
                        alert(msg.Message)
                    }

                },
                error: function (msg) {
                    alert(msg.messages);
                }
            });
        }
        else {

        }
    }
});