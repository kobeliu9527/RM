// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).
//这句的意思是,他会监听浏览器的fetch请求
self.addEventListener('fetch', () => {
    // console.log('fetch触发了');
    //下面的语句会阻止这次请求
    //event.respondWith(new Response(null, { status: 500, statusText: 'Request cancelled' }));
   
});
