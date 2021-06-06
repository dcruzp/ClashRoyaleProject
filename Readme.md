# Clash Royale Web Aplicación 

### Estructura del proyecto por carpetas 

- **Controllers**
    
    En esta carpeta estan los controladores de la aplicacion. 

    ```
        Controllers
            |-JugadorController.cs
            |-ClanController.cs
            |-CartaController.cs
            |-DesafioController.cs
            |-GuerraDeClanesController.cs 
    ``` 
    
    **Jugador Controller**

    El controlador JugadorController.cs tienes los metodos necesarios 
    para la insercion actualizacion y borrado de los jugadores que 
    hay en la base de datos mediantes Get , Put, Post y Delete , tambien tenemos 
    la opcion de considerar la busqueda de un jugador en especifico mediante la 
    accion de Get pasando como paramentro el nombre del jugador 
   

    **CartaController.cs**

    En el controlador CartaController tenemos basicamente lo mismo que en el 
    controlador jugador. Podemos hacer Get, Put, Post y Delete de la base Base de 
    Datos. El Get lo podemos hacer usando como parametro el nombre de la carta de
    la que queremos la informacion , por lo que mediante el nombre podemos hacer
    el siguiente pedido:
    ```
    http://localhost:6600/api/cartas/<nombre_carta>
    ``` 
    Este pedido nos devuelve los datos que hay en el modelo representado
    en la clase ```CartaModels.cs``` que se encuentra en la carpeta  ```Models```
    de la carta ```nombre_carta```. 

    Usando este mismo modelo de la clase ```CartaModels``` podemos
    hacer la actualización de las cartas que hay en la base de datos.
    
    El borrado de una carta en especifico se hace usando su nombre 
    mediante el request, usando el verbo *Delete**
    ```
    http://localhost:6600/api/cartas/<nombre_carta>
    ```

    **ClanController.cs**
    
    El controlador ClanController 

- **DBModels**

    Aquí están todas las clases que me representan la base Datos 
    
    ```
        DBModels
            |- Batalla.cs 
            |- Cartum.cs 
            |- Clan.cs 
            |- clashroyaleContext.cs 
            |- Desafio.cs
            |- Dispone.cs
            |- Donar.cs
            |- Estructura.cs
            |- GuerradeClane.cs
            |- Hechizo.cs
            |- Jugador.cs
            |- Lucha.cs
            |- Miembro.cs
            |- Participa.cs
            |- ParticipaEn.cs
            |- Pertenece.cs
            |- Tropa.cs
    ```

    - La clase  *Batalla.cs* me representa la batalla
        entre dos jugadores 

    - La clase  *Cartum.cs* me representa una carta 

    - La clase *Clan.cs* me representa una Clan 

    - La clase *Desafio.cs* representa un desafío 

    - La clase *Dispone.cs* es una representación de un
    tabla relacional entre un jugador y una carta. para saber 
    las cartas de las que dispone un jugador 

    - La clase *Donar.cs* me representa una donación hecha 
    por una jugador miembro de un clan  de una carta determinada

    - La clase *Estructura.cs* me representa una estructura, 
     que es una especialización de Carta 

    - La clase *GuerradeClane.cs* me representa una guerra
    que puede ocurrir entre varios clanes

    - La clase *Hechizo.cs*  representa una especialización de Carta 

    - La clase *Jugador.cs* me representa un jugador

    - La clase *Lucha.cs* me representa una lucha que 
    se puede dar entre dos jugadores

    - La clase  *Miembro.cs* me representa un los miembros
    de los clanes , es decir la relación de los jugadores con los clanes

    - La clase  *Participa.cs* es para representar la participación 
    de un jugador en un desafió

    - La clase  *ParticipaEn.cs* es para representar la participación 
    de un clan en una guerra 

    - La clase *Pertenece.cs* es para representar los jugadores que pertenecen 
    a un clan. 

    - La Clase *Tropa.cs* es una especialización de carta  
 

 - **Data**

    En esta carpeta se encuentras las clases e interfaces que 
    se usan para hacer inserciones , actualizaciones y barrado de 
    la base de datos
    
 - **Models**
    
    Esto me representa los modelos que se usan para 
    representar los objetos que son enviados al 
    front end 

     
    ```
        Models
            |- CartaModels.cs 
            |- ClanModels.cs 
            |- DesafioModels.cs 
            |- GuerraDeClanesModels.cs 
            |- JugadorModels.cs            
    ```

  - **Data**

    En esta carpeta están todas la interfaces e implementaciones 
    de los datos y las query para manejar la base de datos 
     ```
        Data
            |- ICartaRepository.cs 
            |- IClanRepository.cs 
            |- IDesafioRepository.cs 
            |- IGuerraDeClanesRepository.cs 
            |- IJugadorRepository.cs
            |- ClashRoayaleProfile.cs            
    ```

 ## Diagrama de la base de datos 

   ![imag](img/DBDiagram.png)