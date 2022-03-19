#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>
#include <time.h>

#define MIN 500;
#define MAX 1000;

int main(){
    int a,d;
    printf("Kerek egy egesz szamot: ");
    scanf("%d\n",&a);
    d = a;
    printf("2. feladat: ");
    for(int i = 0;i<7;i++){
        if(d % 10 > 0){
            printf("0");
        }
        else{
            d = d % 10;
            break;
        }
    }
    printf("%d\n",a);

    sleep(5000);

    printf("3. feladat: 5 sec telt el a ket kiiras kozott.\n");

    int result = 0;
    srand(time(0));
    result = rand() % MAX - MIN + MIN;
    printf("4. feladat: %d\n", result);

    bool err = true;
    int b,c;
    do{
        printf("Kerek egy egesz szamot(also): ");
        scanf("%d",&b);
        printf("\nKerek egy egesz szamot(felso): ");
        scanf("%d\n",&c);
        if((b-c) > 0) err = false;
    }while(err);

    srand(time(0));
    result = rand() % b - c + b;
    printf("5. feladat: %d",result);

    return 0;
}