function add() {
    $.ajax({
        type: 'POST',
        url: "/Device/Add",
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

function TypeSelectChanged(select) {
    var index = $("#typeSelect").val();
    if (index != '请选择') {
        var page = index + "Paramter";
        console.log(page);
        $("#paramterDiv").load(page);
    } else {
        $("#paramterDiv").html('');
    }
}