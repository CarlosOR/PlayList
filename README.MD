
## _PlayListProject

[![Build Status](https://travis-ci.org/joemccann/dillinger.svg?branch=master)](https://travis-ci.org/joemccann/dillinger)

- Te api was created in .Net Core V6

- An SQLite database is being used which is being created and filled by migrations when the application starts.
Unit tests i didn't achieve to have good coverage, because of the time.

- The api is not complete at all, the PlayList and Songs endpoints work fine.

- The many-to-many relationship between PlayList and Songs is working and items can be added and removed from the list without any problem, it is only necessary to send the Id.
- For example to call .../Songs/AddList you can use
```json
[
   {
      "name":"Name song",
      "authorId":1,
      "playList":[
         {
            "id":1
         },
         {
            "id":2
         }
      ]
   },
   {
      "name":"Name song2",
      "authorId":4,
      "playList":[
         {
            "id":1
         },
         {
            "id":4
         }
      ]
   }
]
```
