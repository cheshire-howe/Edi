define([
    'jquery',
    'underscore',
    'backbone',
    'models/po',
    'routers/router',
    'components/dataService',
    'text!PurchaseOrder/PoCreate',
    'text!PurchaseOrder/PoLineItem',
    'text!PurchaseOrder/PoDtm'
], function($, _, Backbone, Po, Router, dataService, CreateTmpl, LineItemTmpl, DtmTmpl) {
    var poCreateView = Backbone.View.extend({
        template: _.template(CreateTmpl),
        tagName: 'div',

        events: {
            'click #btnSave': 'createPo',
            'click #btnBack': 'back',
            'click #btnAddLineItem': 'addLineItem',
            'click #btnRemoveLineItem': 'removeLineItem',
            'click #btnAddDtm': 'addDtm',
            'click #btnRemoveDtm': 'removeDtm',
            'click .close': 'closeError'
        },

        $cache: {
            lineItemForm: null,
            dtmForm: null,
            lineItemCount: 0,
            dtmCount: 0
        },

        initialize: function() {
            this.model = new Po();
            this.$cache.lineItemForm = LineItemTmpl;
            this.$cache.dtmForm = DtmTmpl;
        },

        render: function() {
            this.$el.html(this.template(this.model.toJSON()));

            this.addLineItem();
            this.addDtm();

            return this;
        },

        addLineItem: function() {
            var data = {
                count: this.$cache.lineItemCount
            };

            var lineItemTmpl = _.template(this.$cache.lineItemForm);
            $(lineItemTmpl(data)).appendTo(this.$el.find('#line-items')).hide().slideDown();

            this.$cache.lineItemCount++;
        },

        removeLineItem: function(e) {
            if (confirm("Are you sure you want to remove this line item?")) {
                var lineItemNumber = $(e.currentTarget).data('count');
                $('#line-item-' + lineItemNumber).slideUp('slow', function() {
                    this.remove();
                });
            }
        },

        addDtm: function() {
            var data = {
                count: this.$cache.dtmCount
            };
            // so this is gonna find the dtms id and should put whatever text in that node
            var dtmTmpl = _.template(this.$cache.dtmForm);
            // I think we were appending text, but it needed to be a dom node
            // only the second one slides, don't ask me why
            $(dtmTmpl(data)).appendTo(this.$el.find('#dtms')).hide().slideDown();

            this.$cache.dtmCount++;
        },

        removeDtm: function(e) {
            if (confirm("Are you sure you want to remove this date/time?")) {
                var dtmNumber = $(e.currentTarget).data('count');
                $('#dtm-' + dtmNumber).slideUp('slow', function() {
                    this.remove();
                });
            }
        },

        createPo: function(e) {
            e.preventDefault();
            var self = this;

            if (this.model.set(this.getCurrentFormValues(), { validate: true })) {
                dataService.createPo(self.model).then(function(newPo) {
                    self.$cache.lineItemCount = 0;
                    app.pos.add(newPo);
                    app.pos.get(newPo.ID);
                    Router.navigate('', { trigger: true });
                });
            } else {
                $('#errorMsg').text(this.model.validationError);
                $('#validationError').slideDown();
            }
        },

        closeError: function(e) {
            $('#validationError').slideUp();
        },

        back: function() {
            Backbone.history.history.back();
        },

        getCurrentFormValues: function() {
            var lineItems = [];
            for (var i = 0; i < this.$cache.lineItemCount; i++) {
                if ($('#line-item-' + i).is('html *')) {
                    var item = {
                        PO102_QuantityOrdered: $('#PO102_QuantityOrdered-' + i).val(),
                        PO103_UnitOfMeasurement: $('#PO103_UnitOfMeasurement-' + i).val(),
                        PO104_UnitPrice: $('#PO104_UnitPrice-' + i).val(),
                        PO105_BasisOfUnitPriceCode: $('#PO105_BasisOfUnitPriceCode-' + i).val(),
                        PO106_ProductIdQualifier: $('#PO106_ProductIdQualifier-' + i).val(),
                        PO107_ProductID: $('#PO107_ProductID-' + i).val()
                    };
                    lineItems.push(item);
                }
            }
            return {
                Items: lineItems
            };
        }
    });

    return poCreateView;
});