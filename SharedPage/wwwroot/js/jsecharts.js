

//window.echartsext2 = {
//    container: {

//    },
//    api: {
//        init: (id) => {
//            console.log("asdf");
//            // var myChart = echarts.init(document.getElementById(id), theme, opts);
//            // console.log(myChart);
//            //window.EchartsExt.Container[id] = myChart;
//        },
//        setOption: (id, notMerge, lazyUpdate) => {
//            var chart = window.EchartsExt.Container[id];
//            chart.setOption(option, notMerge, lazyUpdate);
//        }
//    }
//};
export class echartsFunc {
    static liChart = {};
    static ResizeListener = {};
    static init(id) {
        var myChart = echarts.init(document.getElementById(id));
        this.liChart[id] = myChart;
    }
    static setOption(id, option, notMerge, lazyUpdate) {
        var chart = this.liChart[id];
        //console.log(option);
        chart.setOption(option);
        try {
        } catch (error) {
        }
        return;
        chart.setOption({
            title: {
                text: 'ECharts 入门示例'
            },
            tooltip: {},
            xAxis: {
                data: ['衬衫', '羊毛衫', '雪纺衫', '裤子', '高跟鞋', '袜子']
            },
            yAxis: {},
            series: [
                {
                    name: '销量',
                    type: 'bar',
                    data: [5, 20, 36, 10, 10, 20]
                }
            ]
        });
    }
    static resize(id) {
        this.liChart[id].resize();
    }
    static addResizeListener(id) {
        this.ResizeListener[id] = () => { this.liChart[id].resize(); };
        window.addEventListener("resize", this.ResizeListener[id]);
        // console.log(id, 'addResizeListener');
    }
    static removeResizeListener(id) {
        window.removeEventListener("resize", this.ResizeListener[id]);
        delete this.liChart[id];
        // console.log(id,'removeResizeListener');
    }
    static addClassForSelect(id) {
        var ele = document.getElementById(id);
        if (!ele.classList.contains('selected')) {
            ele.classList.add('selected');
        }
    }
    static removeClassForSelect() {
        for (var id in this.liChart) {
            var ele = document.getElementById(id);
            if (ele.classList.contains('selected')) {
                ele.classList.remove('selected');
            }
        }

    }
    static getWH(id) {
        var w = document.getElementById(id).clientWidth;
        var h = document.getElementById(id).clientHeight;
        return [w, h]
    }
    static Log(id) {
        console.log(id);
    }

}