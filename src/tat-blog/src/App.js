import './App.css';
import NotFound from './Pages/NotFound';
import BadRequest from './Pages/BadRequest';
import AdminLayout from './Pages/Admin/Layout';
import * as AdminIndex from './Pages/Admin/Index';
import Authors from './Pages/Admin/Authors';
import Categories from './Pages/Admin/Categories';
import Comments from './Pages/Admin/Comments';
import Tags from './Pages/Admin/Tags';
import Posts from './Pages/Admin/Post/Posts';
import Footer from './Components/Footer';
import Index from './Pages/Index';
import Navbar from './Components/Navbar';
import Sidebar from './Components/Sidebar';
import Layout from './Pages/Layout';
import About from './Pages/About';
import Contact from './Pages/Contact';
import RSS from './Pages/Rss';
import {
BrowserRouter as Router,
Routes,
Route,
} from 'react-router-dom';
import Edit from './Pages/Admin/Post/Edit';


function App() {
  return (
    <Router>
      <Routes>         
        <Route path='/400' element={<BadRequest />} />
        <Route path='*' element={<NotFound />} />
        <Route path='/' element={<Layout />}>
          <Route path='/' element={<Index />} />
          <Route path='blog' element={<Index />} />
          <Route path='blog/Contact' element={<Contact />} />
          <Route path='blog/About' element={<About />} />
          <Route path='blog/RSS' element={<RSS />} />
        </Route>
        <Route path='/admin' element={<AdminLayout />} >
          <Route path='/admin' element={<AdminIndex.default />} />
          <Route path='/admin/authors' element={<Authors />} />
          <Route path='/admin/categories' element={<Categories />} />
          <Route path='/admin/comments' element={<Comments />} />
          <Route path='/admin/posts' element={<Posts />} />
          <Route path='/admin/posts/edit' element={<Edit />} />
          <Route path='/admin/posts/edit/:id' element={<Edit />} />
          <Route path='/admin/tags' element={<Tags />} />
        </Route>
      </Routes>
    <Footer />
  </Router>
);
}
export default App;