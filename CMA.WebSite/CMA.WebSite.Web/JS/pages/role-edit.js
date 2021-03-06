﻿var ComponentsDropdowns = function () {
    var e = function () {
        function e(e) {
            return e.id ? "<img class='flag' src='" + App.getGlobalImgPath() + "flags/" + e.id.toLowerCase() + ".png'/>&nbsp;&nbsp;" + e.text : e.text
        }
        function t(e) {
            var t = "<table class='movie-result'><tr>"; return void 0 !== e.posters && void 0 !== e.posters.thumbnail && (t += "<td valign='top'><img src='" + e.posters.thumbnail + "'/></td>"), t += "<td valign='top'><h5>" + e.title + "</h5>", void 0 !== e.critics_consensus ? t += "<div class='movie-synopsis'>" + e.critics_consensus + "</div>" : void 0 !== e.synopsis && (t += "<div class='movie-synopsis'>" + e.synopsis + "</div>"), t += "</td></tr></table>"
        }
        function s(e) {
            return e.title
        }
        $("#select2_sample1").select2({ placeholder: "Select an option", allowClear: !0 }), $("#select2_sample2").select2({ placeholder: "Select a State", allowClear: !0 }), $("#select2_sample3").select2({ placeholder: "Select...", allowClear: !0, minimumInputLength: 1, query: function (e) { var t, s, l, o = { results: [] }; for (t = 1; 5 > t; t++) { for (l = "", s = 0; t > s; s++) l += e.term; o.results.push({ id: e.term + t, text: l }) } e.callback(o) } }), $("#select2_sample4").select2({ placeholder: "Select a Country", allowClear: !0, formatResult: e, formatSelection: e, escapeMarkup: function (e) { return e } }), $("#select2_sample5").select2({ tags: ["red", "green", "blue", "yellow", "pink"] }), $("#select2_sample6").select2({ placeholder: "Search for a movie", minimumInputLength: 1, ajax: { url: "http://api.rottentomatoes.com/api/public/v1.0/movies.json", dataType: "jsonp", data: function (e, t) { return { q: e, page_limit: 10, apikey: "ju6z9mjyajq2djue3gbvv26t" } }, results: function (e, t) { return { results: e.movies } } }, initSelection: function (e, t) { var s = $(e).val(); "" !== s && $.ajax("http://api.rottentomatoes.com/api/public/v1.0/movies/" + s + ".json", { data: { apikey: "ju6z9mjyajq2djue3gbvv26t" }, dataType: "jsonp" }).done(function (e) { t(e) }) }, formatResult: t, formatSelection: s, dropdownCssClass: "bigdrop", escapeMarkup: function (e) { return e } })
    },
    t = function () {
        function e(e) {
            return e.id ? "<img class='flag' src='" + App.getGlobalImgPath() + "flags/" + e.id.toLowerCase() + ".png'/>&nbsp;&nbsp;" + e.text : e.text
        }
        function t(e) {
            var t = "<table class='movie-result'><tr>"; return void 0 !== e.posters && void 0 !== e.posters.thumbnail && (t += "<td valign='top'><img src='" + e.posters.thumbnail + "'/></td>"), t += "<td valign='top'><h5>" + e.title + "</h5>", void 0 !== e.critics_consensus ? t += "<div class='movie-synopsis'>" + e.critics_consensus + "</div>" : void 0 !== e.synopsis && (t += "<div class='movie-synopsis'>" + e.synopsis + "</div>"), t += "</td></tr></table>"
        }
        function s(e) {
            return e.title
        }
        $("#select2_sample_modal_1").select2({ placeholder: "Select an option", allowClear: !0 }), $("#select2_sample_modal_2").select2({ placeholder: "Select a State", allowClear: !0 }), $("#select2_sample_modal_3").select2({ allowClear: !0, minimumInputLength: 1, query: function (e) { var t, s, l, o = { results: [] }; for (t = 1; 5 > t; t++) { for (l = "", s = 0; t > s; s++) l += e.term; o.results.push({ id: e.term + t, text: l }) } e.callback(o) } }), $("#select2_sample_modal_4").select2({ allowClear: !0, formatResult: e, formatSelection: e, escapeMarkup: function (e) { return e } }), $("#select2_sample_modal_5").select2({ tags: ["red", "green", "blue", "yellow", "pink"] }), $("#select2_sample_modal_6").select2({ placeholder: "Search for a movie", minimumInputLength: 1, ajax: { url: "http://api.rottentomatoes.com/api/public/v1.0/movies.json", dataType: "jsonp", data: function (e, t) { return { q: e, page_limit: 10, apikey: "ju6z9mjyajq2djue3gbvv26t" } }, results: function (e, t) { return { results: e.movies } } }, initSelection: function (e, t) { var s = $(e).val(); "" !== s && $.ajax("http://api.rottentomatoes.com/api/public/v1.0/movies/" + s + ".json", { data: { apikey: "ju6z9mjyajq2djue3gbvv26t" }, dataType: "jsonp" }).done(function (e) { t(e) }) }, formatResult: t, formatSelection: s, dropdownCssClass: "bigdrop", escapeMarkup: function (e) { return e } })
    },
    s = function () {
        $(".bs-select").selectpicker({ iconBase: "fa", tickIcon: "fa-check" })
    },
    l = function () {
        $("#my_multi_select1").multiSelect(), $("#my_multi_select2").multiSelect({ selectableOptgroup: !0 })
    };
    return { init: function () { e(), t(), l(), s() } }
}();

jQuery(document).ready(function () {
    ComponentsDropdowns.init()
});