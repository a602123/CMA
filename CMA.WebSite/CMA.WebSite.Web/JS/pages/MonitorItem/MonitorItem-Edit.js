function edit() {
    $.ajax({
        type: 'POST',
        url: "/Device/Edit",
        data: $('#formData').serialize(),
        success: function (data) {
            if (data.State == true) {
                AlertFunc("success", "提交成功");
                setTimeout(function () { window.location = "/Device/index"; }, 3000)
            }
            else {
                AlertFunc("danger", "提交失败");
            }
        },
        error: function () {
            AlertFunc("danger", "出现未知错误");
        }
    });
}