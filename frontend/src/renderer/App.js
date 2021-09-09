import React from 'react'
import { makeStyles } from '@material-ui/core'
import { Admin, Resource } from 'react-admin'
import restProvider from 'ra-data-simple-rest'
import PostList from './components/PostList'
import PostCreate from './components/PostCreate'
import PostEdit from './components/PostEdit'
import UserList from './components/UserList'
import UserCreate from './components/UserCreate'
import UserEdit from './components/UserEdit'

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
  },
  paper: {
    padding: theme.spacing(2),
    textAlign: 'center',
    color: theme.palette.text.secondary,
  },
}));

export default function CenteredGrid() {
  const classes = useStyles();

  return (
    <div className={classes.root}>
      <Admin dataProvider={restProvider('http://localhost:5000')}>
        <Resource
          name='posts'
          list={PostList}
          create={PostCreate}
          edit={PostEdit}
        />
        <Resource
          name='users'
          list={UserList}
          create={UserCreate}
          edit={UserEdit}
        />
      </Admin>
    </div>
  );
}
