// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).
//������˼��,��������������fetch����
self.addEventListener('fetch', () => {
    // console.log('fetch������');
    //�����������ֹ�������
    //event.respondWith(new Response(null, { status: 500, statusText: 'Request cancelled' }));
   
});
