Scheduler.plugin(function (scheduler) {

	scheduler.initCustomLightbox = function (obj, parent, name) {
		var form_stages = {
			"initial":0,
			"load_data":1,
			"save_data":2
		};
		var _frame_id = name + "_here_iframe_";
		parent.config.buttons_left = [];
		parent.config.buttons_right = [];
		parent._getLightbox = parent.getLightbox;
		parent.config.lightbox.sections = [{ type: 'frame', name: 'box'}];

		parent._cust_string_to_date = function (value) {
			return parent.templates.xml_date(value);
		};
		parent._cust_date_to_str = function (value) {
			return parent.templates.xml_format(value);
		};

		function initBox() {
			if (!parent._lightbox) {
				var box = parent._getLightbox();
				var area = box.childNodes[1];
				if (box.className.indexOf("dhx_cal_light_wide") >= 0) {
					area.lastChild.firstChild.style.display = 'none'
				} else {
					area.firstChild.style.display = 'none';
				}
				area.style.height = area.style.height.replace('px', '') - 25 + 'px';
				box.style.height = box.style.height.replace('px', '') - 50 + 'px';
				box.style.width = (+obj.width + 10) + 'px';
				area.style.width = obj.width + 'px';
			}
			return parent._lightbox;

		}
		parent._deep_copy = function (source) {
			if (typeof (source) == "object") {
				if (Object.prototype.toString.call(source) == '[object Date]') {
					var target = new Date(source);
				} else if (Object.prototype.toString.call(source) == '[object Array]') {
					var target = new Array();
				} else {
					var target = new Object();
				}
				for (var i in source) {
					target[i] = scheduler._deep_copy(source[i]);
				}
			}
			else
				var target = source;
			return target;

		};
		if (obj.type == "external") {//server side form
			parent.attachEvent("onBeforeLightbox", function () {
				parent._custom_box_stage = 0;
				return true;
			});
			parent.getLightbox = initBox;

			parent._setLightboxValues = function (frame, id) {
				var frame = document.getElementById(_frame_id);
				try {
					switch (parent._custom_box_stage) {
						case (form_stages.initial):


							if (!parent.dataProcessor) {
								var dp = parent.dataProcessor = new DataProcessor();
								dp.init(parent);
							}
							var dp = parent.dataProcessor || dp;
							var ev = dp._getRowData(id);
							var delem = (obj.view || "").indexOf('?') === -1 ? "?" : "&";
							var form = '<form action=\'/' + obj.view + delem + 'id=' + encodeURIComponent(id) + '\' method=\'POST\'>';
							for (var i in ev) {
								form += '<input type=\'hidden\' name=\'' + i + '\'/>';
							}

							form += '</form>';

							if (!frame.Document)
								var frameBody = frame.contentDocument.body;
							else
								var frameBody = frame.Document.body;
							frameBody.innerHTML = form;
							var count = 0;
							for (var i in ev) {
								frameBody.firstChild.childNodes[count++].value = ev[i];
							}
							frameBody.firstChild.submit();
							break;
						case (form_stages.load_data):



							if (!frame.contentWindow.lightbox) {
								var winobj = frame.contentWindow;
								frame.contentWindow.lightbox = {
									close: function () {
										parent._remove_customBox();
									}

								};
							}
							parent.callEvent('onLightbox', [id]);
							break;
						case (form_stages.save_data):
							if (!frame)
								return;
							var win = frame.contentWindow;
							if (!win || !win.response_data)
								return;


							parent._doLAction(win.response_data);
							parent._remove_customBox();
							break;
					}
				} catch (e) {
					parent._remove_customBox();
					if (window.console)
						console.log(e);
				}
				parent._custom_box_stage++;

			};
			parent._remove_customBox = function () {
				if (parent._lightbox)
					parent.endLightbox(false, parent._lightbox);
				else
					parent.endLightbox(false);


				parent.callEvent("onAfterLightbox", []);
			};
			parent._doLAction = function (item) {
				try {
					if (!item)
						return;
					var updatedEvent = item.data;
					if (updatedEvent.start_date && updatedEvent.end_date) {
						updatedEvent.start_date = parent._cust_string_to_date(item.data.start_date);
						updatedEvent.end_date = parent._cust_string_to_date(item.data.end_date);
					}

					var act = item.action;
					switch (item.action) {
						case "insert":
							parent._loading = true;
							parent.addEvent(updatedEvent);

							parent._loading = false;
							act = "inserted";
							break;
						case "update":
							var oldEvent = parent.getEvent(item.sid);

							for (var i in updatedEvent) {
								oldEvent[i] = updatedEvent[i];
							}
							parent.event_updated(oldEvent);

							parent.updateEvent(item.sid);
							act = "updated";
							break;
						case "delete":
							parent.deleteEvent(item.sid, true);
							act = "deleted";
							break;
						default:
							break;
					}

					parent.dataProcessor.callEvent("onAfterUpdate", [item.sid, act, item.id, item]);

				} catch (e) {
				}

			};
			parent.form_blocks.frame = {
				onload : function(node, event_id, scheduler){
					scheduler._setLightboxValues(node, event_id);
				},
				render: function (sns) {
					return '<div style=\'display:inline-block; height:' + obj.height + 'px\'></div>';
				},
				set_value: function (node, value, ev, config) {
					parent._last_id = ev.id;

					var html = '<iframe id="' + _frame_id + '" '+
						'frameborder="0" ' +
						'onload="' + name + '.form_blocks.frame.onload(this, ' + name + '._last_id, '+name+');" '+
						'src=""';


					if (obj.width || obj.height)
						html += ' style=\'';
					if (obj.width) {
						html += 'width:' + obj.width + 'px;';
						node.style.width = obj.width + 'px';
					}
					if (obj.height) {
						html += 'height:' + obj.height + 'px;';
						node.style.height = obj.height + 'px';
					}
					if (obj.width || obj.height)
						html += ' \'';
					html += '><html></html></iframe>';

					node.innerHTML = html;
					if (obj.className)
						node.className = obj.className;

					return true;
				},
				get_value: function (node, ev) {
					return true;
				},
				focus: function (node) {
					return true;
				}
			};
		} else {
			parent.form_blocks['frame'] = {
				render: function (sns) {
					var res = '<iframe  id="' + name + '_here_iframe_" onload=\'' + name + '._addLightboxInterface(this)\' frameborder=\'0\' src=\'' + obj.view + '\'';
					if (obj.width || obj.height)
						res += ' style=\'';
					if (obj.width)
						res += 'width:' + obj.width + 'px;';
					if (obj.height)
						res += 'height:' + obj.height + 'px;';
					if (obj.width || obj.height)
						res += ' \'';
					res += ' ></iframe>';
					return res;
				},
				set_value: function (node, value, ev) {
					if (node.contentWindow && node.contentWindow.setValues) {

						if (node.contentWindow.document.getElementsByTagName('form').length == 1) {
							node.contentWindow.document.getElementsByTagName('form')[0].reset();
						}
						else {
							var ev = node.contentWindow.getValues();
							for (var i in ev)
								ev[i] = '';
							node.contentWindow.setValues(ev);
						}

						node.contentWindow.setValues(ev);
					}
				},
				get_value: function (node, ev) {
					return parent._deep_copy(node.contentWindow.getValues());
				},
				focus: function (node) {
					return true;
				}
			};

			parent.getLightbox = initBox;

			parent._addLightboxInterface = function (frame) {
				if (!frame.contentWindow.lightbox)
					frame.contentWindow.lightbox = {};

				frame.contentWindow.lightbox.save = function () {
					var ev = parent.getEvent(parent.getState().lightbox_id);
					var changed = frame.contentWindow.getValues();
					for (var i in changed) {
						ev[i] = changed[i];
					}
					parent.endLightbox(true, parent._lightbox);
				};
				frame.contentWindow.lightbox.close = function (argument) {
					parent.endLightbox(false, parent._lightbox);

				};
				frame.contentWindow.lightbox.remove = function () {
					var c = parent.locale.labels.confirm_deleting;
					if (!c || confirm(c)) {
						parent.deleteEvent(parent._lightbox_id);
						parent._new_event = null;

					}
					parent.endLightbox(true, parent._lightbox);
				};


				if (frame.contentWindow.document.getElementsByTagName('form').length == 1) {
					frame.contentWindow.document.getElementsByTagName('form')[0].reset();
				}
				else {
					if (frame.contentWindow.getValues && frame.contentWindow.setValues) {
						var ev = frame.contentWindow.getValues();
						for (var i in ev)
							ev[i] = '';
						frame.contentWindow.setValues(ev);
					}
				}
				if(frame.contentWindow.setValues)
					frame.contentWindow.setValues(parent.getEvent(parent._lightbox_id));
				parent.callEvent('onLightbox', [parent._lightbox_id]);
			};
		};
	};

});