
import  "./localforage.min.js"
import "./echarts.min.js"
export class JsFunc {
    //???
    static liChart = {};
    static SelectList = {};
    static ResizeListener = {};
    static init(id) {
        try {
            var ele = document.getElementById(id);
            if (ele) {
                var myChart = echarts.init(ele);
                if (myChart) {
                    JsFunc.liChart[id] = myChart;
                    console.log(id + ':初始化成功', myChart);
                }
                else {
                    console.log(id+':初始化失败');
                }
            }
            else {
                console.log(id+'元素不存在,无法初始化');
            }
        } catch (e) {
            console.log('初始化失败', e);
        }

    }
    static setOption(id, option, notMerge, lazyUpdate) {
        try {
            var chart = JsFunc.liChart[id];
            if (chart) {
                //chart.showLoading(); 
                //chart.hideLoading();
                let opt = eval('(' + option + ')');
                chart.setOption(opt, notMerge, lazyUpdate);
            }
            else {
                console.log(id + ':没有这个chart实例');
            }
        } catch (e) {
            console.log(id + '执行setOption时候异常了', e);
        }
    }
    static dispose(id) {
        try {
            var chart = JsFunc.liChart[id];
            if (chart) {
                if (chart.isDisposed()) {
                    chart.dispose();
                }
                else {
                    console.log("已经被销毁了");
                }
                delete JsFunc.liChart[id];
            }
            else {
                console.log(id + '图表不存在,无法执行销毁');
            }
        } catch (e) {
            console.log(id + '图表销毁出现异常', e);
        }
    }
    static resize(id) {
        try {
            if (JsFunc.liChart[id]) {
                JsFunc.liChart[id].resize();
            }
        } catch (e) {
            console.log(id + 'resize异常', e);
        }

    }
    static addResizeListener(id) {
        try {
            if (JsFunc.liChart[id]) {
                JsFunc.ResizeListener[id] = () => { JsFunc.liChart[id].resize(); };
                window.addEventListener("resize", JsFunc.ResizeListener[id]);
            }
            else {
                console.log(id + ":添加addEventListener事件失败,因为图表不存在");
            }

        } catch (e) {
            console.log("为" + id + '添加addEventListener事件出现异常', e);
        }


    }
    static removeResizeListener(id) {
        try {
            if (JsFunc.ResizeListener[id]) {
                window.removeEventListener("resize", JsFunc.ResizeListener[id]);
                delete JsFunc.ResizeListener[id];
                console.log(id, "被成功移除");
            }
            else {
                console.log(id, "不存在");
            }
        } catch (e) {
            console.log(id, "异常了", e);
        }

    }
    static addClassForSelect(id, isCtrl) {
        if (!isCtrl) {
            JsFunc.removeClassForSelect();
        }
        var ele = document.getElementById(id);
        if (ele && !ele.classList.contains('selected')) {
            JsFunc.SelectList[id] = id;
            ele.classList.add('selected');
        }
    }
    static removeClassForSelect() {
        for (var id in JsFunc.SelectList) {
            var ele = document.getElementById(id);
            if (ele) {
                if (ele.classList.contains('selected')) {
                    ele.classList.remove('selected');
                }
            }
        }
    }
    static getWH(id) {
        var w = document.getElementById(id).clientWidth;
        var h = document.getElementById(id).clientHeight;
        return [w, h]
    }




}

//window.bb = JsFunc;