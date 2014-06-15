﻿/*
创 建 人     ： 陈 锐
创 建 时 间  ： 2012.07.24
功 能 说 明  ： javascript数据绑定

*/
(function ($) {
    $.cTemplateMethod = $.cTemplateMethod || {};
    $.addcTemplateMethod = function (methodObj) {
        $.cTemplateMethod = $.extend($.cTemplateMethod, methodObj);
    };
    $.extend($.fn, {
        cTemplate: function (option) {
            var setting = $.extend($.cTemplateMethod, option);

            var keyWords = [
                            "if",
                            "else",
                            "end"];

            var dataRegex = /\{[\@\$].+?\}/ig;
            var controlRegex = /\{\#.+?\}/ig;

            var InitTemplate = function (template, setting) {
                var $template = $(template);
                var dataItems = [];
                var controls = [];

                var templateContent = $template.text();
                var cmatch = templateContent.match(controlRegex);

                var controlItems = [];
                var rs = controlRegex.exec(templateContent);
                //IE第一次匹配取不到值
                if (rs != null) {
                    var item = rs[0].substring(2, rs[0].length - 1).replace(/\s/g, "");
                    controlItems.push({ item: item, keyword: readKeyWord(item), index: rs.index, length: rs[0].length });
                }

                while ((rs = controlRegex.exec(templateContent)) != null) {
                    var item = rs[0].substring(2, rs[0].length - 1).replace(/\s/g, "");
                    controlItems.push({ item: item, keyword: readKeyWord(item), index: rs.index, length: rs[0].length });
                }



                var controls = readKeys(controlItems.reverse());

                var match = templateContent.match(dataRegex);
                if (match && match.length > 0) {
                    for (var i = 0; i < match.length; i++) {
                        var replace = match[i];
                        var item = match[i].substring(1, match[i].length - 1);
                        var hasItem = false;
                        for (var j = 0; j < dataItems.length; j++) {
                            if (item == dataItems[j].item) {
                                hasItem = true;
                                break;
                            }
                        }
                        if (!hasItem) {
                            var tree = next(item.replace(/\s/g, ""));
                            dataItems.push({
                                item: item,
                                replace: replace, //new RegExp(replace, "ig"),
                                tree: tree
                            });
                        }
                    }
                }

                function trimTemplate(content) {
                    var parent = /^\s*\<\!\-\-(.+)\-\-\>\s*$/ig;
                }

                function readKeys(keysList, subRead) {
                    var tree = [];
                    while (keysList.length > 0) {
                        var key = keysList[keysList.length - 1].keyword;
                        switch (key) {
                            case "if":
                                tree.push(readIf(keysList));
                                if (subRead)
                                    return tree;
                                break;
                            default:
                                throw "不支持的关键字: " + key + " !";
                                break;
                        }
                    }
                    return tree;
                };

                function readIf(keysList) {
                    var ifhead = keysList.pop();
                    var ifmid;
                    var ifend;
                    var condition = ifhead.item.substring(ifhead.keyword.length + 1, ifhead.item.length - 1);
                    var ifblock = {
                        type: "if",
                        begin: ifhead.index + ifhead.length,
                        condition: next(condition),
                        tblock: "",
                        fblock: "",
                        mainblock: "",
                        tcontrol: [],
                        fcontrol: [],
                        middle: -1,
                        end: -1
                    };
                    var readComplete = false;
                    var hasElse = false;
                    while (!readComplete) {
                        switch (keysList[keysList.length - 1].keyword) {
                            case "else":
                                ifmid = keysList.pop();
                                ifblock.middle = ifmid.index + ifmid.length;
                                hasElse = true;
                                break;
                            case "end":
                                ifend = keysList.pop();
                                ifblock.end = ifend.index + ifend.length;
                                readComplete = true;
                                break;
                            default:
                                if (hasElse) {
                                    ifblock.fcontrol.push(readKeys(keysList, true));
                                }
                                else {
                                    ifblock.tcontrol.push(readKeys(keysList, true));
                                }
                                break;
                        }
                    }
                    ifblock.mainblock = templateContent.substring(ifhead.index, ifblock.end);
                    if (ifmid) {
                        ifblock.tblock = templateContent.substring(ifblock.begin, ifmid.index);
                        ifblock.fblock = templateContent.substring(ifblock.middle, ifend.index);
                    }
                    else {
                        ifblock.tblock = templateContent.substring(ifblock.begin, ifend.index);
                    }
                    return ifblock;
                }

                function readKeyWord(item) {
                    for (var i = 0; i < keyWords.length; i++) {
                        if (item.indexOf(keyWords[i]) == 0) {
                            return keyWords[i];
                        }
                    }
                    return null;
                }

                function next(item, args) {
                    if (!item || item.length == 0)
                        return args ? args : null;
                    item = $.trim(item);
                    var ch = item.charAt(0);
                    var tree = args ? args : [];
                    switch (ch) {
                        case "$":
                            var func = readFunc(item);
                            tree.push({ type: "function", name: func.name, args: next(func.spare) });
                            break;
                        case "@":
                            var para = readParam(item);
                            tree.push({ type: "param", name: para.name });
                            next(para.spare, tree);
                            break;
                        default:
                            var constPara = readConst(item);
                            tree.push({ type: "const", value: constPara.value });
                            next(constPara.spare, tree);
                            break;
                    }
                    return tree;
                };

                function readFunc(item) {
                    var funcName = item.substring(1, item.indexOf("("));
                    var spare = item.substring(1 + funcName.length + 1, item.length - 1);
                    return {
                        name: $.trim(funcName),
                        spare: $.trim(spare)
                    }
                };

                function readParam(item) {
                    var index = item.indexOf(",");
                    var paramName = index < 0 ? item.substring(1, item.length) : item.substring(1, index);
                    var spare = index < 0 ? item.substring(1 + paramName.length, item.length) : item.substring(1 + paramName.length + 1, item.length);
                    return {
                        name: $.trim(paramName),
                        spare: $.trim(spare)
                    }
                };

                function readConst(item) {
                    var index = item.indexOf(",");
                    var value = index < 0 ? item.substring(0, item.length) : item.substring(0, index);
                    var spare = index < 0 ? item.substring(1 + value.length, item.length) : item.substring(value.length + 1, item.length);
                    return {
                        value: parseConstValue($.trim(value)),
                        spare: $.trim(spare)
                    }
                };

                function parseConstValue(value) {
                    if (value.length == 0)
                        return null;
                    if (value[0] == "'" || value[0] == '"') {
                        value = value.slice(1, -1);
                        value = value.replace(/%2C/ig, ",");
                        return value;
                    }
                    switch (value.toLowerCase()) {
                        case "true":
                            return true;
                        case "false":
                            return false;
                        case "null":
                            return null;
                        case "undefined":
                            return undefined;
                    }
                    var num = Number(value);
                    if (!isNaN(num))
                        return num;
                    return value;
                }

                function execute(data, tree) {
                    var result = "";
                    var root = null;
                    if (tree instanceof Array && tree.length > 0) {
                        root = tree[0];
                    }
                    else {
                        root = tree;
                    }
                    if (root == null) {
                        return result;
                    }
                    switch (root.type) {
                        case "function":
                            if (typeof setting[root.name] == "function") {
                                result = setting[root.name].apply(data, makeArgs(data, root.args));
                            }
                            break;
                        case "param":
                            result = getDataItem(data, root.name);
                            break;
                        case "const":
                            result = root.value;
                            break;
                        default:
                            break;
                    }
                    return result;
                };

                function makeArgs(data, tree) {
                    var args = [];
                    if (tree instanceof Array) {
                        for (var i = 0; i < tree.length; i++) {
                            args.push(execute(data, tree[i]));
                        }
                    }
                    return args;
                };

                function getDataItem(data, propertyName) {
                    var path = propertyName.split("."), item, subData = data;
                    for (var i = 0, c = path.length; i < c; i++) {
                        item = path[i];
                        if (subData.hasOwnProperty(item)) {
                            if (i == c - 1)
                                return subData[item];
                            subData = subData[item];
                        }
                    }
                    return propertyName;
                };


                function executeControl(data, controlsBlock, template) {
                    var root = null;
                    if (controlsBlock instanceof Array && controlsBlock.length > 0) {
                        for (var i = 0; i < controlsBlock.length; i++) {
                            template = executeControl(data, controlsBlock[i], template);
                        }
                    }
                    else {
                        root = controlsBlock;
                    }
                    if (root == null) {
                        return template;
                    }
                    switch (root.type) {
                        case "if":
                            template = executeIf(data, root, template);
                            break;
                    }
                    return template;
                };

                function executeIf(data, ifblock, template) {
                    var flag = execute(data, ifblock.condition);
                    if (flag == "true") {
                        template = template.replace(ifblock.mainblock, ifblock.tblock);
                        if (ifblock.tcontrol.length > 0) {
                            template = executeControl(data, ifblock.tcontrol, template);
                        }
                    }
                    else {
                        template = template.replace(ifblock.mainblock, ifblock.fblock);
                        if (ifblock.fcontrol.length > 0) {
                            template = executeControl(data, ifblock.fcontrol, template);
                        }
                    }
                    return template;
                };

                var dataBind = function (data, gData) {
                    var result = templateContent;
                    data = $.extend(data, gData);
                    result = executeControl(data, controls, result);
                    for (var i = 0; i < dataItems.length; i++) {
                        if (result.indexOf(dataItems[i].replace) >= 0) {
                            var value = execute(data, dataItems[i].tree);
                            while (result.indexOf(dataItems[i].replace) >= 0)
                                result = result.replace(dataItems[i].replace, value);
                        }
                    }
                    return result;
                };
                return { DataBind: dataBind };
            };
            return InitTemplate(this, setting);
        }
    });
})(jQuery);