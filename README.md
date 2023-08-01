# Prueba
01/08/2023 Sistema CRUD con data relacionada utilizando NET Core 7 MVC y Entity FC 7.0.9

Permite Crear una Nota y agregarle Categorias como data relacionada.  
Posteriomente podemos listar las notas, ver una nota del listado junto con sus categorías, crear notas y categorías, borrar notas y categorías, editar categorías y editar notas seleccionando o deseleccionado también sus categorías.
La web tiene las entidades que mediante las migraciones de Entity, crea la base de datos en SQL Server, así como las 3 tablas, la tabla de Notas, la de CategoríasNota y la de datas relacionadas.
Se trata de un estudio básico para poder adaptar e implementar esta teoría en cualquier web donde se requiera algo similar.


31/07/2023 Ha sido actualizado con mejoras.
Index              Ya funciona. Muestra todas las Notas y los botones para el CRUD
VerNota            Ya funciona. Se muesta la Nota con sus categorías.
CrearNota          Ya funciona. Se crea una Nota y se marcan los checkboxes de sus categorias. Se graba en la base de datos y automáticamente crea la data relacionada.
BorrarNota         Ya funciona. Borra la Nota y la data relacionada.
Categorias         Ya funciona. Muestra todas las Categorias y los botones para el CRUD
CrearCategoria     Ya funciona. Crea Categorias de las notas.
EditarCategoria    Ya funciona. Permite editar la Categoria y No la data relacionada
Borrar Categoria   Ya funciona. Borra la Categoria y la data relacionada
EditarNota         Ya funciona. Permitir modificar los datos de la Nota, además de poder deseleccionar o seleccionar categorias.






