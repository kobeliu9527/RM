//import html2canvas from 'html2canvas.min.js';
import "./html2canvas.min.js"
//canvas.toDataURL("image/jpeg", 1.0);//后面的表示图片质量 0-1之间,默认是image/png,不支持设置图片质量
export async function toimg(id) {
    const canvas = await html2canvas(document.getElementById(id));
    const dataURL = canvas.toDataURL('image/png');
    return dataURL;
}
