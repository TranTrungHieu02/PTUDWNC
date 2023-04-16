import { get_api } from "./Methods";

export function getPosts(keyword = '',
  pageSize = 10,
  pageNumber = 1,
  sortColumn = '',
  sortOrder = ''){
  return get_api(`https://localhost:7239/api/posts?Keyword=${keyword}&PageSize=${pageSize}&PageNumber=${pageNumber}&SortColumn=${sortColumn}&SortOrder=${sortOrder}`);
}

export function getAuthors(name = '',
  pageSize = 10,
  pageNumber = 1,
  sortColumn = '',
  sortOrder = '') {
  return get_api(`https://localhost:7239/api/authors?name=${name}&PageSize=${pageSize}&PageNumber=${pageNumber}&SortColumn=${sortColumn}&SortOrder=${sortOrder}`);
}

export function getFilter() {
  return get_api('https://localhost:7239/api/posts/get-filter');
  }

export function getPostsFilter(Keyword = '', authorId = '', categoryId = '',
year = '', month  = '', pageSize = 10, PageNumber = 1, sortColumn = '', sortOrder = ''){
  let url = new URL('https://localhost:7239/api/posts/get-posts-filter');
  Keyword !== '' && url.searchParams.append('Keyword', Keyword);
  authorId !== '' && url.searchParams.append('AuthorId', authorId);
  categoryId !== '' && url.searchParams.append('CategoryId', categoryId);
  year !== '' && url.searchParams.append('Year', year);
  month !== '' && url.searchParams.append('Month', month);
  sortColumn !== '' && url.searchParams.append('SortColumn', sortColumn);
  sortOrder !== '' && url.searchParams.append('sortOrder', sortOrder);
  url.searchParams.append('pageSize', pageSize);
  url.searchParams.append('pageNumber', pageNumber);
  return get_api(url.href);
}
