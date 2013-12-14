/*
    create by cray(285025600@qq.com)
    2013/11/24
*/

/* =============================== */
//popover
/* =============================== */

!function ($) {

    "use strict"; // jshint ;_;
    var displayPopover;

    var CoolPopover = function (element, options) {
        var $element = element;
        var $content = options.content;
        var $contentContainer = $content.parent();
        var display = false;
        var me = this;

        $content.find('.class').click(function () {
            me.hide();
        });

        this.id = Math.round(Math.random() * 9999);
        this.show = function () {
            options.trigger = 'manual';
            $element.popover(options);
            $element.popover('show');
            display = true;
            if (displayPopover && displayPopover.hasShow() && displayPopover.id != this.id)
            {
                displayPopover.hide();
            }
            displayPopover = this;
        };
        this.hide = function () {
            var popoverContainer = $content.parent();
            $contentContainer.append($content);
            popoverContainer.append($content.clone());
            $element.popover('destroy');
            display = false;
        };
        this.toggle = function () {
            if (display) {
                this.hide();
                display = false;
            }
            else {
                this.show();
                display = true;
            }
        };
        this.hasShow = function () {
            return display;
        }
    }

    $.fn.coolpopover = function (option) {
        return this.each(function () {
            var $this = $(this)
              , data = $this.data('coolpopover')
              , options = typeof option == 'object' && option
            if (!data) $this.data('coolpopover', (data = new CoolPopover($this, options)))
            if (typeof option == 'string') data[option]()
        })
    }

}(window.jQuery);

/* =============================== */
//typeahead
/* =============================== */

!function ($) {

    "use strict"; // jshint ;_;


    /* TYPEAHEAD PUBLIC CLASS DEFINITION
     * ================================= */

    var Typeahead = function (element, options) {
        this.$element = $(element)
        this.options = $.extend({}, $.fn.typeahead.defaults, options)
        this.matcher = this.options.matcher || this.matcher
        this.sorter = this.options.sorter || this.sorter
        this.highlighter = this.options.highlighter || this.highlighter
        this.updater = this.options.updater || this.updater
        this.source = this.options.source
        this.$menu = $(this.options.menu)
        this.shown = false
        this.listen()
    }

    Typeahead.prototype = {

        constructor: Typeahead

    , select: function () {
        var val = this.$menu.find('.active').attr('data-value')
        this.$element
          .val(this.updater(val))
          .change()
        return this.hide()
    }

    , updater: function (item) {
        return item
    }

    , show: function () {
        var pos = $.extend({}, this.$element.position(), {
            height: this.$element[0].offsetHeight
        })

        this.$menu
          .insertAfter(this.$element)
          .css({
              top: pos.top + pos.height
          , left: pos.left
          })
          .show()

        this.shown = true
        return this
    }

    , hide: function () {
        this.$menu.hide()
        this.shown = false
        return this
    }

    , lookup: function (event) {
        var items

        this.query = this.$element.val()

        if (!this.query || this.query.length < this.options.minLength) {
            return this.shown ? this.hide() : this
        }

        items = $.isFunction(this.source) ? this.source(this.query, $.proxy(this.process, this)) : this.source

        return items ? this.process(items) : this
    }

    , process: function (items) {
        var that = this

        items = $.grep(items, function (item) {
            return that.matcher(item)
        })

        items = this.sorter(items)

        if (!items.length) {
            return this.shown ? this.hide() : this
        }

        return this.render(items.slice(0, this.options.items)).show()
    }

    , matcher: function (item) {
        return ~item.toLowerCase().indexOf(this.query.toLowerCase())
    }

    , sorter: function (items) {
        var beginswith = []
          , caseSensitive = []
          , caseInsensitive = []
          , item

        while (item = items.shift()) {
            if (!item.toLowerCase().indexOf(this.query.toLowerCase())) beginswith.push(item)
            else if (~item.indexOf(this.query)) caseSensitive.push(item)
            else caseInsensitive.push(item)
        }

        return beginswith.concat(caseSensitive, caseInsensitive)
    }

    , highlighter: function (item) {
        var query = this.query.replace(/[\-\[\]{}()*+?.,\\\^$|#\s]/g, '\\$&')
        return item.replace(new RegExp('(' + query + ')', 'ig'), function ($1, match) {
            return '<strong>' + match + '</strong>'
        })
    }

    , render: function (items) {
        var that = this

        items = $(items).map(function (i, item) {
            i = $(that.options.item).attr('data-value', item)
            i.find('a').html(that.highlighter(item))
            return i[0]
        })

        items.first().addClass('active')
        this.$menu.html(items)
        return this
    }

    , next: function (event) {
        var active = this.$menu.find('.active').removeClass('active')
          , next = active.next()

        if (!next.length) {
            next = $(this.$menu.find('li')[0])
        }

        next.addClass('active')
    }

    , prev: function (event) {
        var active = this.$menu.find('.active').removeClass('active')
          , prev = active.prev()

        if (!prev.length) {
            prev = this.$menu.find('li').last()
        }

        prev.addClass('active')
    }

    , listen: function () {
        this.$element
          .on('focus', $.proxy(this.focus, this))
          .on('blur', $.proxy(this.blur, this))
          .on('keypress', $.proxy(this.keypress, this))
          .on('keyup', $.proxy(this.keyup, this))

        if (this.eventSupported('keydown')) {
            this.$element.on('keydown', $.proxy(this.keydown, this))
        }

        this.$menu
          .on('click', $.proxy(this.click, this))
          .on('mouseenter', 'li', $.proxy(this.mouseenter, this))
          .on('mouseleave', 'li', $.proxy(this.mouseleave, this))
    }

    , eventSupported: function (eventName) {
        var isSupported = eventName in this.$element
        if (!isSupported) {
            this.$element.setAttribute(eventName, 'return;')
            isSupported = typeof this.$element[eventName] === 'function'
        }
        return isSupported
    }

    , move: function (e) {
        if (!this.shown) return

        switch (e.keyCode) {
            case 9: // tab
            case 13: // enter
            case 27: // escape
                e.preventDefault()
                break

            case 38: // up arrow
                e.preventDefault()
                this.prev()
                break

            case 40: // down arrow
                e.preventDefault()
                this.next()
                break
        }

        e.stopPropagation()
    }

    , keydown: function (e) {
        this.suppressKeyPressRepeat = ~$.inArray(e.keyCode, [40, 38, 9, 13, 27])
        this.move(e)
    }

    , keypress: function (e) {
        if (this.suppressKeyPressRepeat) return
        this.move(e)
    }

    , keyup: function (e) {
        switch (e.keyCode) {
            case 40: // down arrow
            case 38: // up arrow
            case 16: // shift
            case 17: // ctrl
            case 18: // alt
                break

            case 9: // tab
            case 13: // enter
                if (!this.shown) return
                this.select()
                break

            case 27: // escape
                if (!this.shown) return
                this.hide()
                break

            default:
                this.lookup()
        }

        e.stopPropagation()
        e.preventDefault()
    }

    , focus: function (e) {
        this.focused = true
    }

    , blur: function (e) {
        this.focused = false
        if (!this.mousedover && this.shown) this.hide()
    }

    , click: function (e) {
        e.stopPropagation()
        e.preventDefault()
        this.select()
        this.$element.focus()
    }

    , mouseenter: function (e) {
        this.mousedover = true
        this.$menu.find('.active').removeClass('active')
        $(e.currentTarget).addClass('active')
    }

    , mouseleave: function (e) {
        this.mousedover = false
        if (!this.focused && this.shown) this.hide()
    }

    }


    /* TYPEAHEAD PLUGIN DEFINITION
     * =========================== */

    var old = $.fn.typeahead

    $.fn.typeahead = function (option) {
        return this.each(function () {
            var $this = $(this)
              , data = $this.data('typeahead')
              , options = typeof option == 'object' && option
            if (!data) $this.data('typeahead', (data = new Typeahead(this, options)))
            if (typeof option == 'string') data[option]()
        })
    }

    $.fn.typeahead.defaults = {
        source: []
    , items: 8
    , menu: '<ul class="typeahead dropdown-menu"></ul>'
    , item: '<li><a href="#"></a></li>'
    , minLength: 1
    }

    $.fn.typeahead.Constructor = Typeahead


    /* TYPEAHEAD NO CONFLICT
     * =================== */

    $.fn.typeahead.noConflict = function () {
        $.fn.typeahead = old
        return this
    }


    /* TYPEAHEAD DATA-API
     * ================== */

    $(document).on('focus.typeahead.data-api', '[data-provide="typeahead"]', function (e) {
        var $this = $(this)
        if ($this.data('typeahead')) return
        $this.typeahead($this.data())
    })

}(window.jQuery);

//重写jquery animate
!function ($) {

    "use strict"; // jshint ;_;

    var animate = $.fn.animate;

    $.fn.animate = function () {
        var args = Array.prototype.slice.call(arguments);;
        var $this = $(this);
        var isShow = false;
        if ($this.hasClass("floatBox")) {
            var css = args[0];
            if (css.width == '0px' || css.width == '0')
                isShow = false;
            else
                isShow = true;
            var callback = args[args.length - 1];
            if (typeof callback === "function") {
                args[args.length - 1] = function () {
                    if (isShow)
                        $('body').css("overflow", "hidden");
                    else
                        $('body').css("overflow", "auto");
                    callback.apply(this, arguments);
                }
            }
            else {
                args.push(function () {
                    if (isShow)
                        $('body').css("overflow", "hidden");
                    else
                        $('body').css("overflow", "auto");
                });
            }
        }
        return animate.apply(this, args);
    }

}(window.jQuery);