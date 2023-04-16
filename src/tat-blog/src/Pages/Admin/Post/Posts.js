import React, { useEffect, useState} from "react";
import Table from 'react-bootstrap/Table';
import { Link, useParams, Navigate } from "react-router-dom";
import { getPostsFilter } from "../../../Services/BlogRepository";
import Loading from "../../../Components/Loading";
import PostFilterPane from "../../../Components/Admin/PostFilterPane";
import { isInteger } from '../../../Utils/Utils';
import { useSelector } from "react-redux";

const Posts = () => {
  const [postList, setPostList] = useState([]);
  const [isVisibleLoading, setIsVisibleLoading] = useState(true);
  postFilter = useSelector(state => state.postFilter);

  let { id } = useParams(),
    p = 1,
    ps = 10;

    useEffect(() => {
      document.title = 'Danh sách bài viết';
      getPostsFilter(postFilter.Keyword,
        postFilter.authorId,
        postFilter.categoryId,
        postFilter.year,
        postfilter.month,
        ps, p).then(data => {
          if (data)
            setPostList(data.items);
          else
            setIsVisibleLoading(false);
        });
    },[
      postfilter.Keyword,
      postfilter.authorId,
      postfilter.categoryId,
      postfilter.year,
      postfilter.month,
      p, ps
    ]);

  return (
    <>
    <h1>Danh sách bài viết</h1>
    <PostFilterPane/>
    {isVisibleLoading ? <Loading /> :
      <Table striped responsive bordered>
        <thead>
          <tr>
            <th>Tiêu đề</th>
            <th>Tác giả</th>
            <th>Chủ đề</th>
            <th>Xuất bản</th>
          </tr>
        </thead>
        <tbody>
          {postList.length > 0 ? postList.map((item, index) =>
            <tr key={index}>
              <td>
                <Link
                  to={'/admin/posts/edit/${item.id}'}
                  className='text-bold'>
                  {item.title}
                </Link>
                <p className='text-muted'>{item.shortDescription}</p>
              </td>
                  <td>{item.author.fullName}</td>
                  <td>{item.category.name}</td>
                  <td>{item.published ? "có" : "không"}</td>
                </tr>
              ):
              <tr>
                <td colSpan={4}>
                  <h4 className="text-danger text-center">
                    Không tìm thấy bài viết nào
                  </h4>

                </td>
              </tr>
            }
        </tbody>
      </Table>
    }
    </>
  )
}

export default Posts;
