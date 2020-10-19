const serviceController = (method,url,modalNode) => {
  return Promise.race([
    fetch(url,{
      method,
      headers: {
        'Content-Type': 'application/json;charset=utf-8'
      }
    })
      .then((res) => res.json())
      .then((res) => alert(res.value))
      .catch((res) => alert(res.value))
      .finally(_ => {
        modalNode.classList.toggle("open")
        window.location.reload();
      })
    ,
    new Promise((_,reject) =>
      setTimeout(() => reject(new Error('Timeout')),10000)
    ),
  ]).catch(() => {
    alert('Загрузка занимает слишком много времени, попробуйте перезагрузить страницу.');
  });
}

const searchUrl = (sortName,sortValue) => {
  const searchPath = document.location.search;
  const searchParams = new URLSearchParams(searchPath);
  searchParams.set(sortName,sortValue)
  return searchParams.toString();
}





