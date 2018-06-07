define(function (require) {

    var zrUtil = require('zrender/core/util');
    var axisHelper = require('./axisHelper');

    function getName(obj) {
        if (zrUtil.isObject(obj) && obj.value != null) {
            return obj.value;
        }
        else {
            return obj;
        }
    }
    /**
     * Get Category
     */
    function getCategory() {
        return this.get('type') === 'category'
            && zrUtil.map(this.get('data'), getName);
    }

    /**
     * Format labels
     * @return {Array.<string>}
     */
    function getFormattedLabels() {
        return axisHelper.getFormattedLabels(
            this.axis,
            this.get('axisLabel.formatter')
        );
    }

    return {

        getFormattedLabels: getFormattedLabels,

        getCategory: getCategory
    };
});