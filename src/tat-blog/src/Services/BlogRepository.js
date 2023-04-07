import axios from 'axios';

export async function getPosts(keyword = '', pageSize = 10, pageNumber = 1,
sortColumn = '', sortOrder = '') {
  try {
    const response = await
axios.get(`https://localhost:7239/api/posts?PageSize=${pageSize}&PageNumber=${pageNumber}` );
    const data = response.data;
    if (data.isSuccess)
      return data.result;
    else
      return null;
  } catch (error) {
      console.log('Error', error.message);
    return null;
  }
}