define([
    'underscore',
    'backbone'
], function(_, Backbone) {
    var Po = Backbone.Model.extend({
        url: function() {
            return '/api/PurchaseOrderApi/' + this.get('ID');
        },

        defaults: {
            "ID": 0,
            "CustomerID": "",
            "BEG01_TransactionSetPurposeCode": "",
            "BEG02_PurchaseOrderTypeCode": "",
            "BEG03_PurchaseOrderNumber": "",
            "BEG05_Date": "",
            "CUR01_CurrencyEntityIdentifierCode": "",
            "CUR02_CurrencyCode": "",
            "CTT01_NumberofLineItems": 0,
            "AMT01_AmountQualifierCode": "",
            "AMT02_Amount": 0,
            "Dtms": [],
            "Items": [],
            "Names": [],
            "PoEnvelope": {},
            "Refs": []
        },

        idAttribute: 'ID',

        validate: function(attrs, options) {
            for (var i = 0; i < attrs.Items.length; i++) {
                if (attrs.Items[i].PO107_ProductID === "" ||
                    attrs.Items[i].PO104_UnitPrice === "" ||
                    attrs.Items[i].PO102_QuantityOrdered === "") {
                    return "You are missing values";
                }
            }
        }
    });

    return Po;
});