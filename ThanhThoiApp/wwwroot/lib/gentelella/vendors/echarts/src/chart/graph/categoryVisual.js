define(function (require) {

    return function (ecModel) {
        ecModel.eachSeriesByType('graph', function (seriesModel) {
            var colorList = seriesModel.get('color');
            var CategoryData = seriesModel.getCategoryData();
            var data = seriesModel.getData();

            var categoryNameIdxMap = {};

            CategoryData.each(function (idx) {
                categoryNameIdxMap[CategoryData.getName(idx)] = idx;

                var itemModel = CategoryData.getItemModel(idx);
                var rawIdx = CategoryData.getRawIndex(idx);
                var color = itemModel.get('itemStyle.normal.color')
                    || colorList[rawIdx % colorList.length];
                CategoryData.setItemVisual(idx, 'color', color);
            });

            // Assign category color to visual
            if (CategoryData.count()) {
                data.each(function (idx) {
                    var model = data.getItemModel(idx);
                    var category = model.getShallow('category');
                    if (category != null) {
                        if (typeof category === 'string') {
                            category = categoryNameIdxMap[category];
                        }
                        if (!data.getItemVisual(idx, 'color', true)) {
                            data.setItemVisual(
                                idx, 'color',
                                CategoryData.getItemVisual(category, 'color')
                            );
                        }
                    }
                });
            }
        });
    };
});