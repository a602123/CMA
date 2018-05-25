function operatorFormatter(value, row) {
    return "<a title=\"编辑\" href=\"/Device/Edit/" + row.item.Id + "\"><i class=\"glyphicon glyphicon-edit\"></i></a>" +
        "<a title=\"删除\" onclick=\"Del(" + row.item.Id + ",'" + row.item.Name + "')\"  style=\"margin-left:15px\"><i class=\"glyphicon glyphicon-remove\"></i></a>";
}
function Del(id, name) {
    //alert(id + " " + realname);
    if (confirm("确定删除名称为:" + name + "的设备么？")) {
        $.ajax({
            type: 'Post',
            url: "/Device/Delete",
            data: { id: id },
            dataType: 'json',
            success: function (data) {
                if (data.State == true) {
                    alert(data.Message);
                    location.href = "/Device/Index"
                }
                else {
                    alert(data.Message);
                }
            },
            error: function () {
                alert("删除失败");
            }

        });
    }
}