define('ko.bindingHandlers', ['jquery', 'ko'],
    function ($, ko) {
        ko.bindingHandlers.simplePopup = {
            init: function (element, valueAccessor) {
                var value = ko.utils.unwrapObservable(valueAccessor()),
                    $element = $(element),
                    title,
                    placement;

                if (typeof value === 'object') {
                    title = ko.utils.unwrapObservable(value.title);
                    placement = value.placement || 'bottom';
                }
                else {
                    title = value;
                    placement = 'bottom';
                }

                if (title !== undefined && title !== "" && title !== " ") {
                    $element.attr('title', title);
                    $element.attr('rel', 'tooltip');
                    $element.attr('data-placement', placement);
                    $element.tooltip();
                }
            }
        };
    });