(function (_this) {

    _this.init = function () {
        _this.initEvents();
    }

    _this.initEvents = function () {
        $('.download-report').click(function () {
            var reportId = $(this).data('report-id');
            document.location = `/Report/Download/${reportId}`;
        });
    }

})(window.Report = window.Report || {});