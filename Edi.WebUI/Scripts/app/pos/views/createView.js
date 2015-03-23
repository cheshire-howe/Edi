define([
    'jquery',
    'underscore',
    'backbone',
    'models/po',
    'routers/router',
    'components/dataService',
    'text!PurchaseOrder/PoCreate',
    'text!PurchaseOrder/PoLineItem'
], function($, _, Backbone, Po, Router, dataService, CreateTmpl, LineItemTmpl) {
    var poCreateView = Backbone.View.extend({
        template: _.template(CreateTmpl),
        tagName: 'div',

        events: {
            'click #btnSave': 'createPo',
            'click #btnBack': 'back',
            'click #btnAddLineItem': 'addLineItem',
            'click #btnRemoveLineItem': 'removeLineItem'
        },

        $cache: {
            lineItemForm: null,
            lineItemCount: 0
        },

        initialize: function() {
            this.model = new Po();
            this.$cache.lineItemForm = LineItemTmpl;
        },

        render: function() {
            this.$el.html(this.template(this.model.toJSON()));

            this.addLineItem();

            return this;
        },

        addLineItem: function() {
            this.$cache.lineItemCount++;

            var data = {
                count: this.$cache.lineItemCount
            };

            var lineItemTmpl = _.template(this.$cache.lineItemForm);
            this.$el.find('#line-items').append(lineItemTmpl(data));
        },

        removeLineItem: function(e) {
            if (confirm("Are you sure you want to remove this line item?")) {
                var lineItemNumber = $(e.currentTarget).data('count');
                console.log(lineItemNumber);
                $('#line-item-' + lineItemNumber).remove();
            }
        },

        createPo: function(e) {
            e.preventDefault();
            var self = this;

            if (this.model.set(this.getCurrentFormValues(), { validate: true })) {
                dataService.createPo(self.model).then(function(newPo) {
                    app.pos.add(newPo);
                    app.pos.get(newPo.ID);
                    Router.navigate('', { trigger: true });
                });
            } else {
                $('#validationError').text(this.model.validationError);
            }
        },

        back: function() {
            Backbone.history.history.back();
        },

        getCurrentFormValues: function() {
            return {

            };
        }
    });

    return poCreateView;
});