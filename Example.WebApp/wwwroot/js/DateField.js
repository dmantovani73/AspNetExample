var DateField = function (config) {
    jsGrid.Field.call(this, config);
};

DateField.prototype = new jsGrid.Field({

    css: "date-field",            // redefine general property 'css'
    align: "center",              // redefine general property 'align'

    sorter: function (date1, date2) {
        return new Date(date1) - new Date(date2);
    },

    itemTemplate: function (value) {
        return moment(new Date(value)).format("MM/DD/YYYY");
    },

    insertTemplate: function (value) {
        return this._insertPicker = $("<input>").datepicker({
            defaultDate: new Date(),
            dateFormat: 'mm/dd/yy',
        });
    },

    editTemplate: function (value) {
        return this._editPicker = $("<input>").datepicker({
            dateFormat: 'mm/dd/yy',
        }).datepicker("setDate", new Date(value));
    },

    insertValue: function () {
        var date = this._insertPicker.datepicker("getDate");
        return moment(date).format("YYYY-MM-DD");
    },

    editValue: function () {
        var date = this._editPicker.datepicker("getDate");
        return moment(date).format("YYYY-MM-DD");
    }
});

jsGrid.fields.date = DateField;