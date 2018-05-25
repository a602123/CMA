var helper = {
    Success: function (msg) {
        if (msg.State == true) {
            alert(msg.Message);
        }
    },
    Failure: function (msg) {
        alert(msg.Message);
    },
    OnBegin: function () {
        $("#imgload").show();
    },
    ///通用删除函数
    Delete:function(Url,RId,backUrl)
{
    if(confirm("你确定要删除吗？"))
    {
        $.ajax({
            type: 'POST',
            url: Url,
            data: {"id":RId},
            dataType: 'json',
            success: function (data) {
                if (data.State == true) {
                    alert(data.Message);
                    window.location.href = backUrl;
                }
                else {
                    alert(data.Message);
                }
            },
            error: function () {
                alert("error");
            }
        });
    }
}

}