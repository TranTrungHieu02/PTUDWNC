import React, {useEffect, useState} from "react";
import Pager from "../Components/Pager";
import PostItem from '../Components/PostItem'

import { getPosts } from "../Services/BlogRepository";
import { useLocation } from "react-router-dom";

const Index = () => {
  const[postList, setPostList] = useState([]);
  const [metadata, setMetaData] = useState({});
 function useQuery(){
    const {search} = useLocation();

    return React.useMemo(() => 
    new URLSearchParams(search), [search]);
}
  let query = useQuery(),
  k = query.get('k') ?? '',
  p = query.get('p') ?? 1,
  ps = query.get('ps') ?? 10;


  useEffect(() => {
    document.title = 'Trang chủ';

    getPosts(k, ps, p).then(data => {
      if(data){
        setPostList(data.items);
        setMetaData(data.metadata);
      }
      else
        setPostList([]);
    })
  }, [k, ps, p]);

  useEffect(() => {
    window.scrollTo(0, 0);
  }, [postList]);

  if(postList.length > 0)
    return (
      <div className="p-4">
        {postList.map((item, index) => {
          return (
            <PostItem postItem={item} key={index}/>
          );
        })}
        <Pager postquery={{'keyword': k}} metadata={metadata}/>
      </div>
    );
  else return(
    <></>
  );
}

export default Index;
